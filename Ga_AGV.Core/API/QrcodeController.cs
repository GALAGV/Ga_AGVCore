﻿using Ga_AGV.BLL;
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
    public class QrcodeController : ApiController
    {
        Ga_qrcodeBLL ga_qrcodeBLL = new Ga_qrcodeBLL();
        /// <summary>
        /// 查询任务日志
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonData<Ga_qrcode> Qrcode(int limit, int offset)
        {
            int pageCount = 0;
            JsonData<Ga_qrcode> list = new JsonData<Ga_qrcode>();
            list.rows = ga_qrcodeBLL.ga_QrcodeBLL(ref pageCount, limit, offset);
            list.total = pageCount;
            return list;
        }
    }
}
