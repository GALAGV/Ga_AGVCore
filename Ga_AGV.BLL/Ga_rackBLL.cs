using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ga_AGV.DAL.DataAccess;
using Ga_AGV.Model.DataModel;

namespace Ga_AGV.BLL
{
    public class Ga_rackBLL
    {
        Ga_rackDAL ga_RackDAL = new Ga_rackDAL();

        /// <summary>
        /// 查询货架
        /// </summary>
        /// <param name="pageCount">数据总数</param>
        /// <param name="limit">查询数量</param>
        /// <param name="offset">当前页</param>
        /// <returns></returns>
        public List<Ga_rack> Ga_rackList(ref int pageCount, int limit, int offset)
        {
            return ga_RackDAL.Ga_rackShow(ref pageCount, limit, offset);
        }
    }
}
