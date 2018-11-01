using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.Model.DataModel
{

    /// <summary>
    /// AGV实体类
    /// </summary>
    public class Ga_agv
    {
        /// <summary>
        /// ID
        /// </summary>
        public int agvId { get; set; }

        /// <summary>
        /// AGV编号
        /// </summary>
        public int agvNum { get; set; }

        /// <summary>
        /// agv唯一标识序列号
        /// </summary>
        public string agvSerialNum { get; set; }

        /// <summary>
        /// AGV名称
        /// </summary>
        public string agvName { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string agvIp { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public int agvPort { get; set; }

        /// <summary>
        /// agvX坐标
        /// </summary>
        public long agvY { get; set; }

        /// <summary>
        /// agvY坐标
        /// </summary>
        public long agvX { get; set; }

        /// <summary>
        /// 电池状态
        /// </summary>
        public double agvBatteryStatus { get; set; }

        /// <summary>
        /// agv当前行驶速度
        /// </summary>
        public double agvSpeed { get; set; }

        /// <summary>
        /// 云台状态
        /// </summary>
        public short agvHolderStatus { get; set; }

        /// <summary>
        /// agv当前agv角度方向
        /// </summary>
        public double agvAngel { get; set; }              

        /// <summary>
        /// agv当前扫描区域
        /// </summary>
        public short agvScannerArea { get; set; }

        /// <summary>
        /// agv是否在运行
        /// </summary>
        public byte agvIsRunning { get; set; }

        /// <summary>
        /// agv报警错误码，字符串传输用$分隔
        /// </summary>
        public string agvErrorCode { get; set; }

        /// <summary>
        /// agv注册时间
        /// </summary>
        public long agvCreateTime { get; set; }

        /// <summary>
        /// agv状态码，详见agv状态信息表
        /// </summary>
        public byte agvStatus { get; set; }

        /// <summary>
        /// agv是否在充电
        /// </summary>
        public byte agvIsCharging { get; set; }

        /// <summary>
        /// agv当前任务状态
        /// </summary>
        public byte agvTaskStatus { get; set; }

        /// <summary>
        /// agv当前任务信息
        /// </summary>
        public string agvTaskInfo { get; set; }

        /// <summary>
        /// agv当前二维码信息
        /// </summary>
        public string qrCodeInfo { get; set; }

        /// <summary>
        /// agv最近一次离线时间
        /// </summary>
        public string agvOffLineTime { get; set; }

        /// <summary>
        /// agv最近上线时间
        /// </summary>
        public string agvOnLineTime { get; set; }

        /// <summary>
        /// agv固件版本号
        /// </summary>
        public string agvFirmware { get; set; }       
        
    }
}
