using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ga_AGV.Model.DataModel;
using MySql.Data.MySqlClient;

namespace Ga_AGV.DAL.DataAccess
{
    public class Ga_agvlogDAL
    {
        #region 数据处理

        #region 查询运行日志

        /// <summary>
        /// 查询运行日志（查询）
        /// </summary>
        /// <param name="PageCount">数据总数</param>
        /// <param name="limit">页面大小</param>
        /// <param name="offset">当前页</param>
        /// <param name="log_time">日志时间</param>
        /// <param name="start_time">开始时间</param>
        /// <param name="end_time">结束时间</param>
        /// <param name="agv_num">AGV编号</param>
        /// <param name="task_status">任务状态</param>
        /// <param name="agv_status">AGV状态</param>
        /// <returns></returns>
        public List<Ga_agvloginfo> Ga_AgvloginfosList(ref int PageCount, int limit, int offset, string log_time, string start_time, string end_time, string agv_num, string task_status, string agv_status)
        {
            List<Ga_agvloginfo> ga_s = new List<Ga_agvloginfo>();
            DataTable ds;
            string sql = string.Format("SELECT * FROM `ga_agvlog`.");

            if (log_time == null)
            {
                ds = MySqlHelper.ExecuteDataTable("SELECT table_name FROM information_schema.TABLES WHERE table_name = 'ga_agvloginfo" + DateTime.Now.ToString("yyyyMMdd") + "'");
                if (ds.Rows.Count == 0)
                {
                    return new List<Ga_agvloginfo>();
                }
                sql += string.Format("`ga_agvloginfo{0}` where 0 = 0 ", DateTime.Now.Date.ToString("yyyyMMdd"));
            }
            if (log_time != null)
            {
                ds = MySqlHelper.ExecuteDataTable("SELECT table_name FROM information_schema.TABLES WHERE table_name = 'ga_agvloginfo" + Regex.Replace(log_time, "-", "") + "'");
                if (ds.Rows.Count == 0)
                {
                    return new List<Ga_agvloginfo>();
                }
                sql += string.Format("`ga_agvloginfo{0}` where 0 = 0 ", Regex.Replace(log_time, "-", ""));
            }
            if (start_time != null && end_time != null)
            {
                sql += " and str_to_date(agvLogTime,'%H:%i:%s') between '" + start_time + "' and '" + end_time + "'";
            }
            if (agv_num != null)
            {
                sql += " and agvNum =" + agv_num;
            }
            if (task_status != "0")
            {
                sql += " and agvTaskStatus =" + task_status;
            }
            if (agv_status != "全部")
            {
                if (agv_status == "报警")
                {
                    sql += " and agvErrorCode !=" + 0;
                }
                else if (agv_status == "正常")
                {
                    sql += " and agvErrorCode =" + 0;
                }
            }

            sql += " LIMIT " + offset + "," + limit + "";

            MySqlDataReader mySqlData = MySqlHelper.ExecuteReader(sql);
            while (mySqlData.Read())
            {
                ga_s.Add(new Ga_agvloginfo()
                {
                    agvlogId = Convert.ToInt32(mySqlData["agvlogId"].ToString().Trim()),
                    agvLogTime = mySqlData["agvLogTime"].ToString().Trim(),
                    agvNum = Convert.ToInt32(mySqlData["agvNum"].ToString().Trim()),
                    agvSerialNo = mySqlData["agvSerialNo"].ToString().Trim(),
                    agvIp = mySqlData["agvIp"].ToString().Trim(),
                    agvPort = mySqlData["agvPort"].ToString().Trim(),
                    agvQRInfo = mySqlData["agvQRInfo"].ToString().Trim(),
                    agvX = Convert.ToDouble(mySqlData["agvX"].ToString().Trim()),
                    agvY = Convert.ToDouble(mySqlData["agvY"].ToString().Trim()),
                    agvPowerStatus = Convert.ToDouble(mySqlData["agvPowerStatus"].ToString().Trim()),
                    agvSpeed = Convert.ToDouble(mySqlData["agvSpeed"].ToString().Trim()),
                    agvHolder = Convert.ToInt32(mySqlData["agvHolder"].ToString().Trim()),
                    agvAng = Convert.ToInt32(mySqlData["agvAng"].ToString().Trim()),
                    agvScanner = Convert.ToInt32(mySqlData["agvScanner"].ToString().Trim()),
                    agvIsRunning = Convert.ToInt32(mySqlData["agvIsRunning"].ToString().Trim()),
                    agvErrorCode = Convert.ToInt32(mySqlData["agvErrorCode"].ToString().Trim()),
                    agvStaus = Convert.ToInt32(mySqlData["agvStaus"].ToString().Trim()),
                    agvIsCharging = Convert.ToInt32(mySqlData["agvIsCharging"].ToString().Trim()),
                    agvTaskStatus = Convert.ToInt32(mySqlData["agvTaskStatus"].ToString().Trim()),
                    agvTask = mySqlData["agvTask"].ToString().Trim(),
                });
            }
            mySqlData.Close();

            string count = sql.Replace("*", "Count(*)");
            count = count.Replace("LIMIT", " # ");

            MySqlDataReader mySql = MySqlHelper.ExecuteReader(count);
            while (mySql.Read())
            {
                PageCount = Convert.ToInt32(mySql[0].ToString().Trim());
                break;
            }
            mySql.Close();
            return ga_s;
        }

        #endregion 查询运行日志

        #endregion 数据处理
    }
}