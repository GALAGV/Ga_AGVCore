using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.Model.DataModel
{
    /// <summary>
    /// 货架实体类
    /// </summary>
    public class Ga_rack
    {
        /// <summary>
        /// 货架ID
        /// </summary>
        public int rackId { get; set; }

        /// <summary>
        /// 货架编号
        /// </summary>
        public int rackSerialNum { get; set; }

        /// <summary>
        /// 货架信息
        /// </summary>
        public int rack_qrInfo { get; set; }

        /// <summary>
        /// 1.空闲 2.任务锁定 3.移动中 4.弃用
        /// </summary>
        public int rackStatus { get; set; }

        /// <summary>
        /// 备用
        /// </summary>
        public int rackRemark { get; set; }

        /// <summary>
        /// 如果在移动中对应的agv序号
        /// </summary>
        public int rack_agvSerailNum { get; set; }
    }
}
