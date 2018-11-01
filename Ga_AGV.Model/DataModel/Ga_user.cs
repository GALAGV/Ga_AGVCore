using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.Model.DataModel
{
    /// <summary>
    /// 用户实体类
    /// </summary>
    public class Ga_user
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int userId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string userPassword { get; set; }

        /// <summary>
        /// 用户权限 1.管理员 2.调试人员 3.普通用户 4.监控用户
        /// </summary>
        public int userAuth { get; set; }

        /// <summary>
        /// 添加时间UTCTime
        /// </summary>
        public long userCreateTime { get; set; }

    }
}
