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
    public class Ga_task
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        public int taskId { get; set; }

        /// <summary>
        /// 任务编号
        /// </summary>
        public int taskSerialNo { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public int taskName { get; set; }

        /// <summary>
        /// 执行AGV
        /// </summary>
        public int taskAgv { get; set; }

        /// <summary>
        /// 任务优先级
        /// </summary>
        public int taskPriority { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public int taskStatus { get; set; }
    }
}
