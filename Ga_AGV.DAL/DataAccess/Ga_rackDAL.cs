using Ga_AGV.Model.DataModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ga_AGV.DAL.DataAccess
{
    public class Ga_rackDAL
    {
        public object Log_Time { get; private set; }

        /// <summary>
        /// 查询QR_Code数据信息
        /// </summary>
        /// <param name="pageCount">数据总数</param>
        /// <param name="limit">页面大小</param>
        /// <param name="offset">当前页</param>
        /// <returns></returns>
        public List<Ga_rack> Ga_rackShow(ref int pageCount, int limit, int offset,string rackSerialNum,string rackStatus)
        {
            List<Ga_rack> list = new List<Ga_rack>();
            //DataTable ds;
            var sql = "SELECT * FROM `ga_agv`.`ga_rack` where 1=1";
            //if (Log_Time == null)
            //{
            //    ds = MySqlHelper.ExecuteDataTable("SELECT table_name FROM information_schema.TABLES WHERE table_name = 'ga_taskloginfo" + DateTime.Now.ToString("yyyyMMdd") + "'");
            //    if (ds.Rows.Count == 0)
            //    {
            //        return new List<Ga_rack>();
            //    }
            //    sql += "`ga_taskloginfo" + DateTime.Now.Date.ToString("yyyyMMdd") + "` where 1 = 1";
            //}
            //if (Log_Time != null)
            //{
            //    ds = MySqlHelper.ExecuteDataTable("SELECT table_name FROM information_schema.TABLES WHERE table_name = 'Ga_rack" + Regex.Replace(Log_Time, "-", "") + "'");
            //    if (ds.Rows.Count == 0)
            //    {
            //        return new List<Ga_rack>();
            //    }
            //    sql += "`ga_taskloginfo" + Regex.Replace(Log_Time, "-", "") + "`where 1=1";
            //}
            if (rackSerialNum != null && rackSerialNum != "")
            {
                sql += " and rackSerialNum=" + rackSerialNum + "";
            }
            //if (Time != null && Time != "" && EndTime != null && EndTime != "")
            //{
            //    sql += " and str_to_date(taskLogTime,'%H:%i:%s') between '" + Time + "' and '" + EndTime + "'";
            //}
            if (rackStatus == "全部")
            {
                sql += " and rackStatus=" + 0;
            }
            if (rackStatus != "全部")
            {
                if (rackStatus == "已完成")
                {
                    sql += " and rackStatus=" + 1;
                }
                else if (rackStatus == "未完成")
                {
                    sql += " and rackStatus=" + 2;
                }
                else if (rackStatus == "进行中")
                {
                    sql += " and rackStatus=" + 3;
                }
            }
            sql += " LIMIT " + offset + "," + limit + "";
            MySqlDataReader dd = MySqlHelper.ExecuteReader(sql);
            while (dd.Read())
            {
                list.Add(new Ga_rack()
                {
                    rackId = Convert.ToInt32(dd["rackId"].ToString().Trim()),
                    rackSerialNum = dd["rackSerialNum"].ToString().Trim(),
                    rack_qrInfo = dd["rack_qrInfo"].ToString().Trim(),
                    rackStatus = Convert.ToInt32(dd["rackStatus"].ToString().Trim()),
                    rackRemark = dd["rackRemark"].ToString().Trim(),
                    rack_agvSerailNum = Convert.ToInt32(dd["rack_agvSerailNum"].ToString().Trim()),
                });
            }
            dd.Close();
            var s = "SELECT COUNT(*) FROM `ga_rack`";
            DataTable f = MySqlHelper.ExecuteDataTable(s);
            foreach (DataRow item in f.Rows)
            {
                pageCount = int.Parse(item[0].ToString().Trim());
            }
            return list;
        }
    }
}