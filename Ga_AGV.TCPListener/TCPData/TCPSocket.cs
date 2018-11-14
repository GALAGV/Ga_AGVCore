using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ga_AGV.TCPListener
{
    /// <summary>
    /// Server 服务器
    /// </summary>
    public class TCPSocket
    {
        /// <summary>
        /// 最大连接数
        /// </summary>
        public static int maxConnect;

        /// <summary>
        /// Task 取消开关对象
        /// </summary>
        public static CancellationTokenSource TokenSource;

        /// <summary>
        /// 异步TCP服务器对象
        /// </summary>
        public static AsyncTCPServer TCPServer;




    }
}
