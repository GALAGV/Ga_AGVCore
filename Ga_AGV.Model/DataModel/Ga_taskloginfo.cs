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
        public int tasklogId { get; set; }

        /// <summary>
        /// 日志记录时间
        /// </summary>
        public string taskLogTime { get; set; }

        /// <summary>
        /// 任务名
        /// </summary>
        public string taskName { get; set; }

        /// <summary>
        /// AGV编号
        /// </summary>
        public int taskAgvNum { get; set; }

        /// <summary>
        /// AGV序列号
        /// </summary>
        public string taskAgvSerialNo { get; set; }

        /// <summary>
        /// 任务开始二维码信息
        /// </summary>
        public string taskStartQr { get; set; }

        /// <summary>
        /// 任务开始X轴距离
        /// </summary>
        public string taskStartX { get; set; }

        /// <summary>
        /// 任务开始Y轴距离
        /// </summary>
        public string taskStartY { get; set; }

        /// <summary>
        /// 任务结束二维码信息
        /// </summary>
        public string taskEndQr { get; set; }

        /// <summary>
        /// 任务结束X轴距离
        /// </summary>
        public string taskEndX { get; set; }

        /// <summary>
        /// 任务结束Y轴距离
        /// </summary>
        public string taskEndY { get; set; }

        /// <summary>
        /// 任务状态 1已完成,2已取消，3执行中,0默认
        /// </summary>
        public int taskComplete { get; set; }

        /// <summary>
        /// 任务结束时间
        /// </summary>
        public string taskEndTime { get; set; }
    }
}
