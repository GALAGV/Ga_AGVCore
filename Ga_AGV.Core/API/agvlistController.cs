using Ga_AGV.BLL;
using Ga_AGV.Model.Commonality;
using Ga_AGV.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ga_AGV.Core.API
{
    /// <summary>
    /// AGV管理API
    /// </summary>
    public class agvlistController : ApiController
    {
        Ga_agvBLL Ga_Agv = new Ga_agvBLL();

        /// <summary>
        /// 获取所有AGV数据
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonData<Ga_agv> agv(int limit, int offset)
        {
            int pageCount = 0;
            JsonData<Ga_agv> data = new JsonData<Ga_agv>();
            data.rows = Ga_Agv.GetagvData(ref pageCount, limit, offset);
            data.total = pageCount;
            return data;
        }

        /// <summary>
        /// 添加AGV
        /// </summary>
        /// <param name="agvdata"></param>
        /// <returns></returns>
        public JsonResult Addagv([FromBody] Ga_agv agvdata)
        {
            if (Ga_Agv.agvadd(agvdata))
            {
                return new JsonResult() { Message = "添加成功", Success = true };
            }
            else
            {
                return new JsonResult() { Message = "添加失败", Success = false };
            }
        }
    }
}
