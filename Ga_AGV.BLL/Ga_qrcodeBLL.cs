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

        public List<Ga_qrcode> Ga_QrcodeBLL(ref int PageCount, int limit, int offset)
        {
            return ga_qrcodeDAL.Ga_qrcodesList(ref PageCount, limit, offset);
        }
    }
}