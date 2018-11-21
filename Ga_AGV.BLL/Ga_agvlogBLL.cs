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


        /// <summary>
        ///查询日志
        /// </summary>
        /// <param name="PageCount"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="query_log_time"></param>
        /// <param name="start_time"></param>
        /// <param name="end_time"></param>
        /// <param name="agv_num"></param>
        /// <param name="task_status"></param>
        /// <param name="agv_status"></param>
        /// <returns></returns>
        public List<Ga_agvloginfo> Ga_AgvloginfosBLL(ref int PageCount, int limit, int offset, string query_log_time, string start_time, string end_time, string agv_num, string task_status, string agv_status)
        {
            return ga_AgvlogDAL.Ga_AgvloginfosList(ref PageCount, limit, offset, query_log_time, start_time, end_time, agv_num, task_status, agv_status);
        }

        /// <summary>
        /// 导出日志
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="Db"></param>
        /// <returns></returns>
        public string log(string TableName, string Db,bool type)
        {
            return ga_AgvlogDAL.ExportagvLog(TableName,Db, type);
        }

        /// <summary>
        /// 查询日志是否存在
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public bool TableExistx(string TableName)
        {
            return ga_AgvlogDAL.TableExistx(TableName);
        }
    }
}