using Ga_AGV.DAL.DataAccess;
using Ga_AGV.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.BLL
{
    public class Ga_agvBLL
    {
        Ga_agvDAL GeagvtDAL = new Ga_agvDAL();

        /// <summary>
        /// 获取所有AGV数据
        /// </summary>
        /// <param name="pageCount"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public List<Ga_agv> GetagvData(ref int pageCount, int limit, int offset,int agvNum)
        {
            return GeagvtDAL.GetagvList(ref pageCount, limit, offset, agvNum);
        }

        /// <summary>
        /// 添加AGV
        /// </summary>
        /// <returns></returns>
        public bool agvadd(Ga_agv agv)
        {
           return GeagvtDAL.Addagv(agv);
        }

        /// <summary>
        /// 删除agv
        /// </summary>
        /// <param name="agv"></param>
        /// <returns></returns>
        public bool delete(Ga_agv agv)
        {
            return GeagvtDAL.agvdelete(agv);
        }


        /// <summary>
        /// agv批量删除
        /// </summary>
        /// <param name="agv"></param>
        /// <returns></returns>
        public bool deletelist(List<Ga_agv> agv)
        {
            return GeagvtDAL.agvdeletelist(agv);
        }

        /// <summary>
        /// 编辑AGV
        /// </summary>
        /// <param name="agv"></param>
        /// <returns></returns>
        public bool edit(Ga_agv agv)
        {
            return GeagvtDAL.editagv(agv);
        }
    }
}
