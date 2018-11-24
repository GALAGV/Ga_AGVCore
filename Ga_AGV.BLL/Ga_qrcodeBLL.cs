using Ga_AGV.DAL.DataAccess;
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
        public List<Ga_qrcode> Ga_QrcodeBLL(ref int PageCount, int limit, int offset, string qrID, int qrStatus)
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

        /// <summary>
        /// 批量添加二维码
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Ga_Adds(Ga_map map)
        {
            List<Ga_qrcode> qr = new List<Ga_qrcode>();

            int X = map.Map_x / map.Qr_x;
            int Y = map.Map_y / map.Qr_y;

            for (int i = 1; i < map.Map_x / map.Qr_x; i++)
            {
                for (int k = 1; k < map.Map_y / map.Qr_y; k++)
                {
                    qr.Add(new Ga_qrcode { qrX = i * map.Qr_x, qrY = k * map.Qr_y });
                }
            }
            return ga_qrcodeDAL.Ga_AddsQRcode(map.Map_name, map.Map_x, map.Map_y, map.Widget_info, qr);
        }
    }
}