using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ga_AGV.Model.DataModel;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace Ga_AGV.DAL.DataAccess
{
    public class Ga_tasklogDAl
    {
        /// <summary>
        /// 查询任务日志（查询）
        /// </summary>
        /// <param name="pageCount">数据总数</param>
        /// <param name="limit">页面大小</param>
        /// <param name="offset">当前页</param>
        /// <param name="AGVNum">AGV编号</param>
        /// <param name="Time">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <param name="Log_Time">日志时间</param>
        /// <param name="taskComplete">任务状态</param>
        /// <returns></returns>
        public List<Ga_taskloginfo> Ga_taskloglist(ref int pageCount, int limit, int offset, string AGVNum, string Time, string EndTime, string Log_Time, string taskComplete)
        {
            List<Ga_taskloginfo> list = new List<Ga_taskloginfo>();
            DataTable ds;
            var sql = "SELECT * FROM `ga_agvlog`.";
            if (Log_Time == null)
            {
                ds = MySqlHelper.ExecuteDataTable("SELECT table_name FROM information_schema.TABLES WHERE table_name = 'ga_taskloginfo" + DateTime.Now.ToString("yyyyMMdd") + "'");
                if (ds.Rows.Count == 0)
                {
                    return new List<Ga_taskloginfo>();
                }
                sql += "`ga_taskloginfo" + DateTime.Now.Date.ToString("yyyyMMdd") + "` where 1 = 1";
            }
            if (Log_Time != null)
            {
                ds = MySqlHelper.ExecuteDataTable("SELECT table_name FROM information_schema.TABLES WHERE table_name = 'ga_agvloginfo" + Regex.Replace(Log_Time, "-", "") + "'");
                if (ds.Rows.Count == 0)
                {
                    return new List<Ga_taskloginfo>();
                }
                sql += "`ga_taskloginfo" + Regex.Replace(Log_Time, "-", "") + "`where 1=1";
            }
            if (AGVNum != null && AGVNum != "")
            {
                sql += " and taskAgvNum=" + AGVNum + "";
            }
            if (Time != null && Time != "" && EndTime != null && EndTime != "")
            {
                sql += " and str_to_date(taskLogTime,'%H:%i:%s') between '" + Time + "' and '" + EndTime + "'";
            }
            if (taskComplete == "全部")
            {
                sql += " and 1=1";
            }
            if (taskComplete != "全部")
            {
                if (taskComplete == "已完成")
                {
                    sql += " and taskComplete=" + 1;
                }
                else if (taskComplete == "已取消")
                {
                    sql += " and taskComplete=" + 2;
                }
                else if (taskComplete == "进行中")
                {
                    sql += " and taskComplete=" + 3;
                }
            }

            sql += " LIMIT " + offset + "," + limit + "";
            MySqlDataReader dd = MySqlHelper.ExecuteReader(sql);
            while (dd.Read())
            {
                list.Add(new Ga_taskloginfo()
                {
                    TasklogId = Convert.ToInt32(dd["tasklogId"].ToString().Trim()),
                    TaskLogTime = dd["taskLogTime"].ToString().Trim(),
                    TaskName = dd["taskName"].ToString().Trim(),
                    TaskAgvNum = Convert.ToInt32(dd["taskAgvNum"].ToString().Trim()),
                    TaskAgvSerialNo = dd["taskAgvSerialNo"].ToString().Trim(),
                    TaskStartQr = dd["taskStartQr"].ToString().Trim(),
                    TaskStartX = dd["taskStartX"].ToString().Trim(),
                    TaskStartY = dd["taskStartY"].ToString().Trim(),
                    TaskEndQr = dd["taskEndQr"].ToString().Trim(),
                    TaskEndX = dd["taskEndX"].ToString().Trim(),
                    TaskEndY = dd["taskEndY"].ToString().Trim(),
                    TaskComplete = Convert.ToInt32(dd["taskComplete"].ToString().Trim()),

                    TaskEndTime = dd["taskEndTime"].ToString().Trim(),
                });
            }
            dd.Close();
            string count = sql.Replace("*", "Count(*)");
            count = count.Replace("LIMIT", " # ");

            MySqlDataReader mySql = MySqlHelper.ExecuteReader(count);
            while (mySql.Read())
            {
                pageCount = Convert.ToInt32(mySql[0].ToString().Trim());
                break;
            }
            return list;
        }
    }
}