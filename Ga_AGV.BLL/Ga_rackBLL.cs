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
        public List<Ga_rack> Ga_rackList(ref int pageCount, int limit, int offset, string rackSerialNum, string rackStatus)
        {
            return ga_RackDAL.Ga_rackShow(ref pageCount, limit, offset, rackSerialNum,rackStatus);
        }
        /// <summary>
        /// 添加货架
        /// </summary>
        /// <returns></returns>
        public bool rackadd(Ga_rack rack)
        {
            return ga_RackDAL.Addrack(rack);
        }

        /// <summary>
        /// 删除货架
        /// </summary>
        /// <param name="agv"></param>
        /// <returns></returns>
        public bool delete(Ga_rack rack)
        {
            return ga_RackDAL.rackdelete(rack);
        }


        /// <summary>
        /// 货架批量删除
        /// </summary>
        /// <param name="agv"></param>
        /// <returns></returns>
        public bool deletelist(List<Ga_rack> rack)
        {
            return ga_RackDAL.rackdeletelist(rack);
        }

        /// <summary>
        /// 编辑货架
        /// </summary>
        /// <param name="agv"></param>
        /// <returns></returns>
        public bool edit(Ga_rack rack)
        {
            return ga_RackDAL.editrack(rack);
        }
    }
}
