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
        public string agvIsRunning { get; set; }

        /// <summary>
        /// PBS
        /// </summary>
        public string PBS { get; set; }

        /// <summary>
        /// 云台状态
        /// </summary>
        public string agvHolder { get; set; }

        /// <summary>
        /// AGV报警信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// agv速度
        /// </summary>
        public double agvspeed { get; set; }

        /// <summary>
        /// 电压
        /// </summary>
        public double voltage { get; set; }



        public bool Success { get; set; }

    }

    public class Jsontaskinfo : JsonagvInfo
    {
        /// <summary>
        /// 所在X轴
        /// </summary>
        public string agvX { get; set; }

        /// <summary>
        /// 所在Y轴
        /// </summary>
        public string agvY { get; set; }

        /// <summary>
        /// 当前任务
        /// </summary>
        public string Task { get; set; }

        /// <summary>
        /// 任务开始X轴距离
        /// </summary>
        public string taskStartX { get; set; }

        /// <summary>
        /// 任务开始Y轴距离
        /// </summary>
        public string taskStartY { get; set; }


        /// <summary>
        /// 任务结束X轴距离
        /// </summary>
        public string taskEndX { get; set; }

        /// <summary>
        /// 任务结束Y轴距离
        /// </summary>
        public string taskEndY { get; set; }
    }

}
