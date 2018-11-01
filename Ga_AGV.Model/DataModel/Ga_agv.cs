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
        /// AGV注册时间
        /// </summary>
        public long agvCreateTime { get; set; }

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

        /// <summary>
        /// agv当前行驶速度
        /// </summary>
        public float agvSpeed { get; set; }

        /// <summary>
        /// agv当前角度方向
        /// </summary>
        public float agvAngel { get; set; }
    }
}
