using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.Model.Commonality
{
    /// <summary>
    /// AGV状态 JSON数据类
    /// </summary>
    public class JsonagvInfo
    {
        /// <summary>
        /// AGV编号
        /// </summary>
        public int agvNum { get; set; }

        /// <summary>
        /// 固件版本
        /// </summary>
        public string agvFirmware { get; set; }

        /// <summary>
        /// 二维码
        /// </summary>
        public string qrcode { get; set; }

        /// <summary>
        /// 是否在运行
        /// </summary>
        public int agvIsRunning { get; set; }

        /// <summary>
        /// PBS
        /// </summary>
        public int PBS { get; set; }

        /// <summary>
        /// 云台状态
        /// </summary>
        public int agvHolder { get; set; }

        /// <summary>
        /// AGV报警信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// agv速度
        /// </summary>
        public string agvspeed { get; set; }

        /// <summary>
        /// 电压
        /// </summary>
        public string voltage { get; set; }

    }
}
