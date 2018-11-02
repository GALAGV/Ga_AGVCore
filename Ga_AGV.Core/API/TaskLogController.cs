using Ga_AGV.Model.Commonality;
using Ga_AGV.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ga_AGV.BLL;

namespace Ga_AGV.Core.API
{
    public class TaskLogController : ApiController
    {
        /// <summary>
        /// 查询任务日志
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonData<Ga_taskloginfo> TaskLog(int limit, int offset)
        {
            int pageCount = 0;
            JsonData<Ga_taskloginfo> list = new JsonData<Ga_taskloginfo>();
            list.rows = Ga_agvlogBLL.list(ref pageCount, limit, offset);
            list.total = pageCount;
            return list;
        }
    }
}
