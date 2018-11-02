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
    public class agvlogController : ApiController
    {
        Ga_agvlogBLL ga_AgvlogBLL = new Ga_agvlogBLL();

        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="limit">页面大小</param>
        /// <param name="offset">当前页</param>
        /// <returns></returns>
        [HttpGet]
        public JsonData<Ga_agvloginfo> Log(int limit, int offset)
        {
            int PageCount = 0;
            JsonData<Ga_agvloginfo> data = new JsonData<Ga_agvloginfo>();
            data.rows = ga_AgvlogBLL.Ga_AgvloginfosBLL(ref PageCount, limit, offset);
            data.total = PageCount;
            return data;
        }

    }
}
