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
    public class AGVSystemController : ApiController
    {
        private Ga_qrcodeBLL BLL = new Ga_qrcodeBLL();

        /// <summary>
        /// 获取所有QR_Code数据
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonData<Ga_qrcode> QRCodeShow(int limit, int offset)
        {
            int pageCount = 0;
            JsonData<Ga_qrcode> data = new JsonData<Ga_qrcode>
            {
                rows = BLL.Ga_QrcodeBLL(ref pageCount, limit, offset),
                total = pageCount
            };

            return data;
        }

        /// <summary>
        /// 更改二维码信息
        /// </summary>
        /// <returns></returns>
        public bool UpdateQRCode()
        {
            return true;
        }
    }
}