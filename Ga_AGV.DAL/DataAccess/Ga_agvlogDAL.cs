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

        #endregion 数据处理


        public bool TableExistx(string TableName)
        {
           DataTable ds = MySqlHelper.ExecuteDataTable("SELECT table_name FROM information_schema.TABLES WHERE table_name = '"+ TableName + "'");
            return ds.Rows.Count > 0;
        }

        #region 导出日志
        
        /// <summary>
        /// 导出日志
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="Db">数据库名</param>
        /// <returns></returns>
        public string ExportagvLog(string TableName, string Db,bool type)
        {
            string dropSql = string.Format("DROP TABLE IF EXISTS {0}.`{1}`;", Db, TableName);
            StringBuilder sqlText = new StringBuilder(dropSql);
            string createSql = string.Format("CREATE TABLE IF NOT EXISTS {0}.`{1}`", Db, TableName);
            sqlText.Append(createSql);
            sqlText.Append(" (");
            string sql = string.Format("SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{0}' AND TABLE_SCHEMA = '{1}'", TableName, Db);
            MySqlDataReader mr = MySqlHelper.ExecuteReader(sql);
            string keyStr = "";
            int colCount = 0;
            while (mr.Read())
            {
                sqlText.Append(" `");
                sqlText.Append(mr["COLUMN_NAME"].ToString());
                sqlText.Append("` ");
                sqlText.Append(mr["COLUMN_TYPE"].ToString().ToUpper());
                if (mr["IS_NULLABLE"].ToString() == "NO")
                {
                    sqlText.Append(" NOT NULL ");
                }
                else
                {
                    sqlText.Append(" DEFAULT NULL ");
                }
                sqlText.Append(mr["EXTRA"].ToString().ToUpper());
                if (!string.IsNullOrEmpty(mr["COLUMN_COMMENT"].ToString()))
                {
                    sqlText.Append(" COMMENT ");
                    sqlText.Append("'");
                    sqlText.Append(mr["COLUMN_COMMENT"].ToString());
                    sqlText.Append("'");
                }
                sqlText.Append(",");
                if (mr["COLUMN_KEY"].ToString() == "PRI")
                {
                    keyStr = "PRIMARY KEY(`" + (type == true ? "agvlogId" : "tasklogId") + "`)";
                }
                colCount++;
            }
            sqlText.Append(keyStr);
            sqlText.Append(") ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8 ROW_FORMAT = COMPACT;");
            mr.Close();

            sql = string.Format("SELECT * FROM {0}.`{1}`", Db, TableName);
            mr = MySqlHelper.ExecuteReader(sql);

            string Delstr = string.Format("DELETE FROM {0}.`{1}`;", Db, TableName);
            StringBuilder insertSqlText = new StringBuilder(Delstr);
            int getName = 0;
            while (mr.Read())
            {
                if (getName == 0)
                {
                    string insertSql = string.Format("INSERT INTO {0}.`{1}` (", Db, TableName);
                    insertSqlText.Append(insertSql);
                    for (int i = 0; i < colCount; i++)
                    {
                        insertSqlText.Append(" `");
                        insertSqlText.Append(mr.GetName(i));
                        if (i < colCount - 1)
                        {
                            insertSqlText.Append("`, ");
                        }
                        else
                        {
                            insertSqlText.Append("`) VALUES");
                        }
                    }
                    getName = 1;
                }

                for (int i = 0; i < colCount; i++)
                {
                    if (i == 0)
                    {
                        insertSqlText.Append("('");
                    }
                    else
                    {
                        insertSqlText.Append(" '");
                    }

                    insertSqlText.Append(mr[i].ToString());

                    if (i < colCount - 1)
                    {
                        insertSqlText.Append("', ");
                    }
                    else
                    {
                        insertSqlText.Append("'),");
                    }
                }
            }
            mr.Close();
            insertSqlText.Remove(insertSqlText.Length - 1, 1);
            insertSqlText.Append(";");
            return sqlText.ToString() + insertSqlText.ToString();
        }
        #endregion

    }
}