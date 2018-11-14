﻿using Ga_AGV.BLL;
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
        Ga_settingBLL ga_Setting = new Ga_settingBLL();

        /// <summary>
        /// 开启 Server 线程
        /// </summary>
        public bool LoadTCP()
        {
            try
            {
                if (TCPSocket.TCPServer != null)
                {
                    if (TCPSocket.TCPServer.IsRunning)
                    {
                        return false;
                    }
                }
                TCPSocket.TokenSource = new CancellationTokenSource(); //一种多线程取消任务开关对象
                Task.Factory.StartNew(TCPMonitoring, TCPSocket.TokenSource.Token);//启动线程
                return true;
            }
            catch (Exception)
            {
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
                TCPSocket.TokenSource.Cancel();
                return true;
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
            //TCPSocket.TCPServer.DataReceived += TCPServer_DataReceived;
            TCPSocket.TCPServer.Start();
        }

        private void TCPServer_DataReceived(object sender, AsyncEventArgs e)
        {
            //e._state.RecvDataBuffer;

        }









    }
}
