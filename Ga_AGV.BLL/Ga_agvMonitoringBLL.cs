using Ga_AGV.DAL.DataAccess;
using Ga_AGV.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.BLL
{
    public class Ga_agvMonitoringBLL
    {
        private Ga_agvMonitoringDAL DAL = new Ga_agvMonitoringDAL();

        public List<Ga_qrcode> BLLShowQRplace(ref int pageCount)
        {
            return DAL.DALShowQRplace(ref pageCount);
        }
    }
}