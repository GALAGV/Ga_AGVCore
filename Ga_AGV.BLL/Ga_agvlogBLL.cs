using Ga_AGV.DAL.DataAccess;
using Ga_AGV.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.BLL
{
    public class Ga_agvlogBLL
    {
        private Ga_agvlogDAL ga_AgvlogDAL = new Ga_agvlogDAL();

        public List<Ga_agvloginfo> Ga_AgvloginfosBLL(ref int PageCount, int limit, int offset, string query_log_time, string start_time, string end_time, string agv_num, string task_status, string agv_status)
        {
            return ga_AgvlogDAL.Ga_AgvloginfosList(ref PageCount, limit, offset, query_log_time, start_time, end_time, agv_num, task_status, agv_status);
        }
    }
}