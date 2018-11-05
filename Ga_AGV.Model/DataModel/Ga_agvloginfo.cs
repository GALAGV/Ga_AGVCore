using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.Model.DataModel
{
    /// <summary>
    /// 运行日志实体类
    /// </summary>
    public class Ga_agvloginfo
    {
        /// <summary>
        /// 日志ID
        /// </summary>
        public int agvlogId { get; set; }

        /// <summary>
        /// 日志产生时间
        /// </summary>
        public string agvLogTime { get; set; }

        /// <summary>
        /// AGV编号
        /// </summary>
        public int agvNum { get; set; }

        /// <summary>
        /// AGV序列号
        /// </summary>
        public string agvSerialNo { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string agvIp { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public string agvPort { get; set; }

        /// <summary>
        /// 二维码信息
        /// </summary>
        public string agvQRInfo { get; set; }

        /// <summary>
        /// 所在X轴
        /// </summary>
        public double agvX { get; set; }

        /// <summary>
        /// 所在Y轴
        /// </summary>
        public double agvY { get; set; }

        /// <summary>
        /// 电压        
        /// </summary>
        public double agvPowerStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double agvSpeed { get; set; }

        /// <summary>
        /// 云台状态
        /// </summary>
        public int agvHolder { get; set; }

        /// <summary>
        /// 角度
        /// </summary>
        public double agvAng { get; set; }

        /// <summary>
        /// 扫描区域
        /// </summary>
        public int agvScanner { get; set; }

        /// <summary>
        /// 是否在运行
        /// </summary>
        public int agvIsRunning { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public int agvErrorCode { get; set; }

        /// <summary>
        /// 本身状态信息
        /// </summary>
        public int agvStaus { get; set; }

        /// <summary>
        /// 是否在充电
        /// </summary>
        public int agvIsCharging { get; set; }

        /// <summary>
        /// 任务状态 1,已完成,2，已取消，3执行中,0默认
        /// </summary>
        public int agvTaskStatus { get; set; }

        /// <summary>
        ///任务 
        /// </summary>
        public string agvTask { get; set; }
    }
}
