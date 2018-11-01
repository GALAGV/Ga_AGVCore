using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.Model.DataModel
{
    /// <summary>
    /// 配置实体类
    /// </summary>
   public class Ga_setting
    {
        /// <summary>
        /// 配置ID
        /// </summary>
        public int settingId { get; set; }


        /// <summary>
        /// 配置项
        /// </summary>
        public string settingItem { get; set; }


        /// <summary>
        /// 配置值
        /// </summary>
        public string settingVlaue { get; set; }
    }
}
