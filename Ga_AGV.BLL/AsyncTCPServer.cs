using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.BLL
{
    internal class AsyncTCPServer : IDisposable
    {
        #region Fields

        /// <summary>
        /// 服务器程序允许的最大客户端连接数
        /// </summary>
        private int _maxClient;

        /// <summary>
        /// 当前的连接的客户端数
        /// </summary>
        private int _clientCount;

        /// <summary>
        /// 服务器使用的异步TcpListener
        /// </summary>
        private TcpListener _listener;

        /// <summary>
        /// 客户端会话列表
        /// </summary>
        public List<TCPClientState> _clients;

        private bool disposed = false;

        /// <summary>
        /// 链接数量改变
        /// </summary>
        internal bool _clientChange;

        #endregion Fields

        #region Properties

        /// <summary>
        /// 服务器是否正在运行
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// 监听的IP地址
        /// </summary>
        public IPAddress Address { get; private set; }

        /// <summary>
        /// 监听的端口
        /// </summary>
        public int Port { get; private set; }

        /// <summary>
        /// 通信使用的编码
        /// </summary>
        public Encoding Encoding { get; set; }

        #endregion Properties

        #region 构造函数

        /// <summary>
        /// 异步TCP服务器
        /// </summary>
        /// <param name="listenPort">监听的端口</param>
        public AsyncTCPServer(int listenPort)
            : this(IPAddress.Any, listenPort, 10)
        {
        }

        /// <summary>
        /// 异步TCP服务器
        /// </summary>
        /// <param name="localEP">监听的终结点</param>
        public AsyncTCPServer(IPEndPoint localEP)
            : this(localEP.Address, localEP.Port, 10)
        {
        }

        /// <summary>
        /// 异步TCP服务器
        /// </summary>
        /// <param name="localIPAddress">监听的IP地址</param>
        /// <param name="listenPort">监听的端口</param>
        public AsyncTCPServer(IPAddress localIPAddress, int listenPort, int maxConnect)
        {
            Address = localIPAddress;
            Port = listenPort;
            this.Encoding = Encoding.Default;

            _clients = new List<TCPClientState>();
            _maxClient = maxConnect;
            _listener = new TcpListener(Address, Port);
            _listener.AllowNatTraversal(true);
        }

        #endregion 构造函数

        #region Method

        /// <summary>
        /// 启动服务器
        /// </summary>
        public void Start()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                _listener.Start();
                _listener.BeginAcceptTcpClient(
                  new AsyncCallback(HandleTcpClientAccepted), _listener);
            }
        }

        /// <summary>
        /// 启动服务器
        /// </summary>
        /// <param name="backlog">
        /// 服务器所允许的挂起连接序列的最大长度
        /// </param>
        public void Start(int backlog)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                _listener.Start(backlog);
                _listener.BeginAcceptTcpClient(
                  new AsyncCallback(HandleTcpClientAccepted), _listener);
            }
        }

        /// <summary>
        /// 停止服务器
        /// </summary>
        public void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                _listener.Stop();
                lock (_clients)
                {
                    //关闭所有客户端连接
                    CloseAllClient();
                }
            }
        }

        /// <summary>
        /// 处理客户端连接的函数
        /// </summary>
        /// <param name="ar"></param>
        private void HandleTcpClientAccepted(IAsyncResult ar)
        {
            if (IsRunning)
            {
                TcpListener server = (TcpListener)ar.AsyncState;
                Socket client = server.EndAcceptSocket(ar);

                //检查是否达到最大的允许的客户端数目
                if (_clientCount >= _maxClient)
                {
                    //C-TODO 触发事件
                    RaiseOtherException(null);
                }
                else
                {
                    TCPClientState state = new TCPClientState(client);
                    lock (_clients)
                    {
                        _clients.Add(state);
                        _clientCount++;
                        _clientChange = true;
                        RaiseClientConnected(state); //触发客户端连接事件
                    }
                    state.RecvDataBuffer = new byte[client.ReceiveBufferSize];
                    //开始接受来自该客户端的数据
                    client.BeginReceive(state.RecvDataBuffer, 0, state.RecvDataBuffer.Length, SocketFlags.None,
                        new AsyncCallback(HandleDataReceived), state);
                }
                //接受下一个请求
                server.BeginAcceptSocket(new AsyncCallback(HandleTcpClientAccepted), ar.AsyncState);
            }
        }

        /// <summary>
        /// 数据接受回调函数
        /// </summary>
        /// <param name="ar"></param>
        private void HandleDataReceived(IAsyncResult ar)
        {
            if (IsRunning)
            {
                TCPClientState state = (TCPClientState)ar.AsyncState;
                Socket client = state.ClientSocket;
                try
                {
                    //如果两次开始了异步的接收,所以当客户端退出的时候
                    //会两次执行EndReceive
                    if (ar != null)
                    {
                        int recv = client.EndReceive(ar);
                        if (recv == 0)
                        {
                            //触发事件 (关闭客户端)
                            Close(state);
                            RaiseNetError(state);
                            return;
                        }
                        //处理已经读取的数据 ps:数据在state的RecvDataBuffer中
                        //触发数据接收事件
                        ASCIIEncoding ascii = new ASCIIEncoding();
                        state.Datagram = ascii.GetString(state.RecvDataBuffer).Trim("\0".ToCharArray());//去除字符串中的\0
                        RaiseDataReceived(state);
                    }
                    else
                    {
                        return;
                    }
                }
                catch (SocketException ex)
                {
                    //异常处理
                    Console.WriteLine(ex.ToString());
                    RaiseNetError(state);
                }

                //接收之前清空byte[] 数据
                Array.Clear(state.RecvDataBuffer, 0, state.RecvDataBuffer.Length);
                //继续接收来自来客户端的数据
                try
                {
                    client.BeginReceive(state.RecvDataBuffer, 0, state.RecvDataBuffer.Length, SocketFlags.None, new AsyncCallback(HandleDataReceived), state);
                }
                catch (Exception ex)
                {
                    Close(state);
                    RaiseNetError(state);
                    Console.WriteLine(ex.ToString());
                    return;
                }
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="state">接收数据的客户端会话</param>
        /// <param name="data">数据报文</param>
        public void Send(TCPClientState state, byte[] data)
        {
            RaisePrepareSend(state);
            if (!IsRunning)
                throw new InvalidProgramException("This TCP Scoket server has not been started.");
            if (state.ClientSocket == null)
                throw new ArgumentNullException("client");

            if (data == null)
                throw new ArgumentNullException("data");
            state.ClientSocket.BeginSend(data, 0, data.Length, SocketFlags.None,
             new AsyncCallback(SendDataEnd), state.ClientSocket);
        }

        /// <summary>
        /// 发送数据完成处理函数
        /// </summary>
        /// <param name="ar">目标客户端Socket</param>
        private void SendDataEnd(IAsyncResult ar)
        {
            ((Socket)ar.AsyncState).EndSend(ar);
            RaiseCompletedSend(null);
        }

        #endregion Method

        #region 事件

        /// <summary>
        /// 与客户端的连接已建立事件
        /// </summary>
        public event EventHandler<AsyncEventArgs> ClientConnected;

        /// <summary>
        /// 与客户端的连接已断开事件
        /// </summary>
        public event EventHandler<AsyncEventArgs> ClientDisconnected;

        /// <summary>
        /// 触发客户端连接事件
        /// </summary>
        /// <param name="state"></param>
        private void RaiseClientConnected(TCPClientState state)
        {
            if (ClientConnected != null)
            {
                ClientConnected(this, new AsyncEventArgs(state));
            }
        }

        /// <summary>
        /// 触发客户端连接断开事件
        /// </summary>
        /// <param name="client"></param>
        private void RaiseClientDisconnected(TCPClientState state)
        {
            if (ClientDisconnected != null)
            {
                ClientDisconnected(this, new AsyncEventArgs("连接断开"));
            }
        }

        /// <summary>
        /// 接收到数据事件
        /// </summary>
        public event EventHandler<AsyncEventArgs> DataReceived;

        private void RaiseDataReceived(TCPClientState state)
        {
            if (DataReceived != null)
            {
                DataReceived(this, new AsyncEventArgs(state));
            }
        }

        /// <summary>
        /// 发送数据前的事件
        /// </summary>
        public event EventHandler<AsyncEventArgs> PrepareSend;

        /// <summary>
        /// 触发发送数据前的事件
        /// </summary>
        /// <param name="state"></param>
        private void RaisePrepareSend(TCPClientState state)
        {
            if (PrepareSend != null)
            {
                PrepareSend(this, new AsyncEventArgs(state));
            }
        }

        /// <summary>
        /// 数据发送完毕事件
        /// </summary>
        public event EventHandler<AsyncEventArgs> CompletedSend;

        /// <summary>
        /// 触发数据发送完毕的事件
        /// </summary>
        /// <param name="state"></param>
        private void RaiseCompletedSend(TCPClientState state)
        {
            if (CompletedSend != null)
            {
                CompletedSend(this, new AsyncEventArgs(state));
            }
        }

        /// <summary>
        /// 网络错误事件
        /// </summary>
        public event EventHandler<AsyncEventArgs> NetError;

        /// <summary>
        /// 触发网络错误事件
        /// </summary>
        /// <param name="state"></param>
        private void RaiseNetError(TCPClientState state)
        {
            if (NetError != null)
            {
                NetError(this, new AsyncEventArgs(state));
            }
        }

        /// <summary>
        /// 异常事件
        /// </summary>
        public event EventHandler<AsyncEventArgs> OtherException;

        /// <summary>
        /// 触发异常事件
        /// </summary>
        /// <param name="state"></param>
        private void RaiseOtherException(TCPClientState state, string descrip)
        {
            if (OtherException != null)
            {
                OtherException(this, new AsyncEventArgs(descrip, state));
            }
        }

        private void RaiseOtherException(TCPClientState state)
        {
            RaiseOtherException(state, "");
        }

        #endregion 事件

        #region Close

        /// <summary>
        /// 关闭一个与客户端之间的会话
        /// </summary>
        /// <param name="state">需要关闭的客户端会话对象</param>
        public void Close(TCPClientState state)
        {
            if (state != null)
            {
                state.Close();
                _clients.Remove(state);
                _clientChange = true;
                _clientCount--;
                RaiseClientDisconnected(state);
            }
        }

        /// <summary>
        /// 关闭所有的客户端会话,与所有的客户端连接会断开
        /// </summary>
        public void CloseAllClient()
        {
            while (true)
            {
                if (_clients.Count > 0)
                {
                    Close(_clients[0]); ;
                }
                else break;
            }

            _clientCount = 0;
            _clientChange = true;
            _clients.Clear();
        }

        #endregion Close

        #region 释放

        /// <summary>
        /// Performs application-defined tasks associated with freeing,
        /// releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release
        /// both managed and unmanaged resources; <c>false</c>
        /// to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    try
                    {
                        Stop();
                        if (_listener != null)
                        {
                            _listener = null;
                        }
                    }
                    catch (SocketException)
                    {
                        RaiseOtherException(null);
                    }
                }
                disposed = true;
            }
        }

        #endregion 释放

    }

    public class TCPClientState
    {
        #region 字段

        /// <summary>
        /// 接收数据缓冲区
        /// </summary>
        private byte[] _recvBuffer;

        /// <summary>
        /// 客户端发送到服务器的报文
        /// 注意:在有些情况下报文可能只是报文的片断而不完整
        /// </summary>
        private string _datagram;

        /// <summary>
        /// 客户端的Socket
        /// </summary>
        private Socket _clientSock;

        #endregion 字段

        #region 属性

        /// <summary>
        /// 接收数据缓冲区
        /// </summary>
        public byte[] RecvDataBuffer
        {
            get
            {
                return _recvBuffer;
            }
            set
            {
                _recvBuffer = value;
            }
        }

        /// <summary>
        /// 存取会话的报文
        /// </summary>
        public string Datagram
        {
            get
            {
                return _datagram;
            }
            set
            {
                _datagram = value;
            }
        }

        /// <summary>
        /// 获得与客户端会话关联的Socket对象
        /// </summary>
        public Socket ClientSocket
        {
            get
            {
                return _clientSock;
            }
        }

        #endregion 属性

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cliSock">会话使用的Socket连接</param>
        public TCPClientState(Socket cliSock)
        {
            _clientSock = cliSock;
        }

        /// <summary>
        /// 初始化数据缓冲区
        /// </summary>
        public void InitBuffer()
        {
            if (_recvBuffer == null && _clientSock != null)
            {
                _recvBuffer = new byte[_clientSock.ReceiveBufferSize];
            }
        }

        /// <summary>
        /// 关闭会话
        /// </summary>
        public void Close()
        {
            //关闭数据的接受和发送
            _clientSock.Shutdown(SocketShutdown.Both);

            //清理资源
            _clientSock.Close();
        }
    }

    /// <summary>
    /// 异步TcpListener TCP服务器事件参数类
    /// </summary>
    public class AsyncEventArgs : EventArgs
    {
        /// <summary>
        /// 提示信息
        /// </summary>
        public string _msg;

        /// <summary>
        /// 客户端状态封装类
        /// </summary>
        public TCPClientState _state;

        /// <summary>
        /// 是否已经处理过了
        /// </summary>
        public bool IsHandled { get; set; }

        public AsyncEventArgs(string msg)
        {
            this._msg = msg;
            IsHandled = false;
        }

        public AsyncEventArgs(TCPClientState state)
        {
            this._state = state;
            IsHandled = false;
        }

        public AsyncEventArgs(string msg, TCPClientState state)
        {
            this._msg = msg;
            this._state = state;
            IsHandled = false;
        }
    }
}