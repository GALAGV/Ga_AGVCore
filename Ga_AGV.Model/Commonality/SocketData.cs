using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.Model.Commonality
{
    /// <summary>
    /// TCP JSON数据类
    /// </summary>
    public class SocketData
    {
        /// <summary>
        /// IP
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 最大连接数
        /// </summary>
        public int maxConnect { get; set; }

        /// <summary>
        /// 服务器监听是否在运行
        /// </summary>
        public bool IsRunning { get; set; }

        /// <summary>
        /// 在线数量
        /// </summary>
        public int OnClientCount { get; set; }
    }
}
