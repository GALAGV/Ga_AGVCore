﻿using Ga_AGV.DAL.DataAccess;
using Ga_AGV.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.BLL
{
    public class Ga_qrcodeBLL
    {
        private Ga_qrcodeDAL ga_qrcodeDAL = new Ga_qrcodeDAL();

        /// <summary>
        /// show
        /// </summary>
        /// <param name="PageCount"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public List<Ga_qrcode> Ga_QrcodeBLL(ref int PageCount, int limit, int offset, string qrID, string qrStatus)
        {
            return ga_qrcodeDAL.Ga_qrcodesList(ref PageCount, limit, offset, qrID, qrStatus);
        }

        public bool Ga_AddQRcodeBLL(Ga_qrcode qr)
        {
            return ga_qrcodeDAL.Ga_AddQRcode(qr);
        }

        public bool Ga_UpQRcodeBLL(Ga_qrcode qr)
        {
            return ga_qrcodeDAL.Ga_UpQRcode(qr);
        }

        public bool Ga_DelQRcodeBLL(List<Ga_qrcode> qr)
        {
            return ga_qrcodeDAL.Ga_DelQRcode(qr);
        }
    }
}