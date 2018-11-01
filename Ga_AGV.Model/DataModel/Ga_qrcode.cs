using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.Model.DataModel
{
    /// <summary>
    /// 二维码实体类
    /// </summary>
    public class Ga_qrcode
    {
        /// <summary>
        /// 二维码ID
        /// </summary>
        public int qrId { get; set; }

        /// <summary>
        /// 二维码信息
        /// </summary>
        public int qrInfo { get; set; }

        /// <summary>
        /// 二维码X值
        /// </summary>
        public int qrX { get; set; }

        /// <summary>
        /// 二维码Y值
        /// </summary>
        public int qrY { get; set; }

        /// <summary>
        /// 二维码状态
        /// </summary>
        public int qrStatus { get; set; }

        /// <summary>
        /// 二维码备注
        /// </summary>
        public int qrRemark { get; set; }
    }
}
