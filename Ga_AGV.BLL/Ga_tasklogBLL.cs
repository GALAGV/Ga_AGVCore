using Ga_AGV.DAL.DataAccess;
using Ga_AGV.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.BLL
{
    public class Ga_tasklogBLL
    {
        /// <summary>
        /// 查询任务日志
        /// </summary>
        /// <param name="pageCount">数据总数</param>
        /// <param name="limit">查询数量</param>
        /// <param name="offset">当前页</param>
        /// <returns></returns>
        public  List<Ga_taskloginfo> TaskLoglist(ref int pageCount, int limit, int offset)
        {
            return Ga_tasklogDAl.list(ref pageCount, limit, offset);
        }


    }
}
