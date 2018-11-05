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
    public class RackController : ApiController
    {
        Ga_rackBLL ga_RackBLL = new Ga_rackBLL();
        /// <summary>
        /// 查询任务日志
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonData<Ga_rack> Rack(int limit, int offset)
        {
            int pageCount = 0;
            JsonData<Ga_rack> list = new JsonData<Ga_rack>();
            list.rows = ga_RackBLL.Ga_rackList(ref pageCount, limit, offset);
            list.total = pageCount;
            return list;
        }
    }
}
