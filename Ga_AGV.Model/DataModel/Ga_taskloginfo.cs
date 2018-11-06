using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.Model.DataModel
{
    /// <summary>
    /// 任务日志实体类
    /// </summary>
    public class Ga_taskloginfo
    {
        /// <summary>
        /// 日志ID
        /// </summary>
        public int TasklogId { get; set; }

        /// <summary>
        /// 日志记录时间
        /// </summary>
        public string TaskLogTime { get; set; }

        /// <summary>
        /// 任务名
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// AGV编号
        /// </summary>
        public int TaskAgvNum { get; set; }

        /// <summary>
        /// AGV序列号
        /// </summary>
        public string TaskAgvSerialNo { get; set; }

        /// <summary>
        /// 任务开始二维码信息
        /// </summary>
        public string TaskStartQr { get; set; }

        /// <summary>
        /// 任务开始X轴距离
        /// </summary>
        public string TaskStartX { get; set; }

        /// <summary>
        /// 任务开始Y轴距离
        /// </summary>
        public string TaskStartY { get; set; }

        /// <summary>
        /// 任务结束二维码信息
        /// </summary>
        public string TaskEndQr { get; set; }

        /// <summary>
        /// 任务结束X轴距离
        /// </summary>
        public string TaskEndX { get; set; }

        /// <summary>
        /// 任务结束Y轴距离
        /// </summary>
        public string TaskEndY { get; set; }

        /// <summary>
        ///任务状态 1,已完成,2，已取消，3执行中,0默认
        /// </summary>
        public int TaskComplete { get; set; }

        /// <summary>
        /// 任务结束时间
        /// </summary>
        public string TaskEndTime { get; set; }
    }
}