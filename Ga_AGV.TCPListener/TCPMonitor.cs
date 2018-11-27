using Ga_AGV.BLL;
using Ga_AGV.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ga_AGV.TCPListener
{
    /// <summary>
    /// 异步TCP服务器
    /// </summary>
    public class TCPMonitor
    {
        private Ga_settingBLL ga_Setting = new Ga_settingBLL();

        /// <summary>
        /// 开启 Server 线程
        /// </summary>
        public bool LoadTCP(ref string msg)
        {
            try
            {
                if (TCPSocket.TCPServer != null)
                {
                    if (TCPSocket.TCPServer.IsRunning)
                    {
                        msg = "服务器正在运行！";
                        return false;
                    }
                }
                //TCPSocket.TokenSource = new CancellationTokenSource(); //一种多线程取消任务开关对象
                //Task.Factory.StartNew(TCPMonitoring, TCPSocket.TokenSource.Token);//启动线程
                TCPMonitoring();
                return TCPSocket.TCPServer.IsRunning;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                closeTCP();
                return false;
            }
        }

        /// <summary>
        /// 关闭 Server 线程
        /// </summary>
        public bool closeTCP()
        {
            try
            {
                TCPSocket.TCPServer.Stop();//停止服务器
                TCPSocket.TCPServer.Dispose();//释放资源
                //TCPSocket.TokenSource.Cancel();
                return !TCPSocket.TCPServer.IsRunning;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 创建TCP异步服务器
        /// </summary>
        public void TCPMonitoring()
        {
            List<Ga_setting> ga_s = ga_Setting.Ga_Settings();
            string Address = ga_s.FirstOrDefault(x => x.settingItem.Equals("ServerIp")).settingVlaue;
            int Port = Convert.ToInt32(ga_s.FirstOrDefault(x => x.settingItem.Equals("ServePort")).settingVlaue);
            TCPSocket.maxConnect = Convert.ToInt32(ga_s.FirstOrDefault(x => x.settingItem.Equals("ServemaxConnect")).settingVlaue);
            TCPSocket.TCPServer = new AsyncTCPServer(IPAddress.Parse(Address), Port, TCPSocket.maxConnect);
            TCPSocket.TCPServer.ClientConnected += TCPServer_ClientConnected;
            TCPSocket.TCPServer.DataReceived += TCPServer_DataReceived;
            TCPSocket.TCPServer.Start();
        }

        private void TCPServer_ClientConnected(object sender, AsyncEventArgs e)
        {
            object s = sender;
        }

        private void TCPServer_DataReceived(object sender, AsyncEventArgs e)
        {
            object s = sender;
        }

        private void GetMessageList(byte[] byteStr, ref List<byte[]> MessageList)
        {
            int byteLen = byteStr.Length;
            for (int i = 0; i < byteLen; i++)
            {
                if (i < byteLen - 5 && byteStr[i] == 0x23 && byteStr[i + 1] == 0x79 && byteStr[i + 2] == 0x6C)//找到帧头
                {
                    int getDateLen = byteStr[i + 3] * 256 + byteStr[i + 4];//需要获取的消息长度
                    byte[] messageByte = new byte[getDateLen];
                    if (getDateLen <= byteLen - i && byteStr[i + byteLen - 2] == 0x7E && byteStr[i + byteLen - 1] == 0x23)//确认长度足够以及帧尾正确
                    {
                        for (int k = 0; k < getDateLen; k++)
                        {
                            messageByte[k] = byteStr[i + k];
                        }

                        MessageList.Add(messageByte);
                        i = i + getDateLen - 1;
                    }
                }
            }
        }
    }
}