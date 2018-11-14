using Ga_AGV.Model.Commonality;
using Ga_AGV.TCPListener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ga_AGV.Core.API
{
    public class ServicesController : ApiController
    {
        TCPMonitor TCPMonitors = new TCPMonitor();

        /// <summary>
        /// 关闭 Server
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult close()
        {
            if (TCPMonitors.closeTCP())
            {
                return new JsonResult() { Message = "关闭成功", Success = true };
            }
            else
            {
                return new JsonResult() { Message = "关闭失败", Success = false };
            }
        }

        /// <summary>
        /// 开启 Server
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult start()
        {
            if (TCPMonitors.LoadTCP())
            {
                return new JsonResult() { Message = "启动成功", Success = true };
            }
            else
            {
                return new JsonResult() { Message = "启动失败", Success = false };
            }
        }

        /// <summary>
        /// 网络监听
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public SocketData Socket()
        {
            return new SocketData (){ Address = TCPSocket.TCPServer.Address.ToString(), Port= TCPSocket.TCPServer.Port, maxConnect= TCPSocket.maxConnect, IsRunning= TCPSocket.TCPServer.IsRunning , OnClientCount= TCPSocket.TCPServer._clients.Count };
        }
    }
}
