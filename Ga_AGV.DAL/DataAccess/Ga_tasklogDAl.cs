using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ga_AGV.Model.DataModel;
using System.Data;
using MySql.Data.MySqlClient;

namespace Ga_AGV.DAL.DataAccess
{
    public class Ga_tasklogDAl
    {
        /// <summary>
        /// 查询任务日志
        /// </summary>
        /// <param name="pageCount">数据总数</param>
        /// <param name="limit">页面大小</param>
        /// <param name="offset">当前页</param>
        /// <returns></returns>
        public List<Ga_taskloginfo> list(ref int pageCount, int limit, int offset)
        {
            List<Ga_taskloginfo> list = new List<Ga_taskloginfo>();
            var sql = "SELECT * FROM `ga_agvlog`.`ga_taskloginfo20181105` LIMIT " + offset + "," + limit + "";
            MySqlDataReader dd = MySqlHelper.ExecuteReader(sql);
            while (dd.Read())
            {
                list.Add(new Ga_taskloginfo()
                {
                    tasklogId = int.Parse(dd["tasklogId"].ToString().Trim()),
                    taskLogTime = dd["taskLogTime"].ToString().Trim(),
                    taskName = dd["taskName"].ToString().Trim(),
                    taskAgvNum = int.Parse(dd["taskAgvNum"].ToString().Trim()),
                    taskAgvSerialNo = dd["taskAgvSerialNo"].ToString().Trim(),
                    taskStartQr = dd["taskStartQr"].ToString().Trim(),
                    taskStartX = dd["taskStartX"].ToString().Trim(),
                    taskStartY = dd["taskStartY"].ToString().Trim(),
                    taskEndQr = dd["taskEndQr"].ToString().Trim(),
                    taskEndX = dd["taskEndX"].ToString().Trim(),
                    taskEndY = dd["taskEndY"].ToString().Trim(),
                    taskComplete = int.Parse(dd["taskComplete"].ToString().Trim()),

                    taskEndTime = dd["taskEndTime"].ToString().Trim(),
                });
            }
            dd.Close();
            var s = "SELECT COUNT(*) FROM `ga_agvlog`.`ga_taskloginfo20181105`";
            DataTable f = MySqlHelper.ExecuteDataTable(s);
            foreach (DataRow item in f.Rows)
            {
                pageCount = int.Parse(item[0].ToString().Trim());
            }
            return list;
        }
        ///// <summary>
        ///// 条件查询
        ///// </summary>
        ///// <param name="pageCount">数据总数</param>
        ///// <param name="limit">页面大小</param>
        ///// <param name="offset">当前页</param>
        ///// <returns></returns>
        //public static List<Ga_taskloginfo> Ga_taskLogCondition(int AGVNum,ref int pageCount, int limit, int offset)
        //{
        //    List<Ga_taskloginfo> list = new List<Ga_taskloginfo>();
        //    var sql = "SELECT * FROM `ga_taskloginfow` here 1=1";
        //    if (AGVNum!=0)
        //    {
        //        sql+= "and taskAgvNum="+AGVNum+ "  LIMIT " + offset + "," + limit + "";
        //    }
        //    MySqlDataReader dd = MySqlHelper.ExecuteReader(sql);
        //    while (dd.Read())
        //    {
        //        list.Add(new Ga_taskloginfo()
        //        {
        //            tasklogId = int.Parse(dd["tasklogId"].ToString().Trim()),
        //            taskLogTime = dd["taskLogTime"].ToString().Trim(),
        //            taskName = dd["taskName"].ToString().Trim(),
        //            taskAgvNum = int.Parse(dd["taskAgvNum"].ToString().Trim()),
        //            taskAgvSerialNo = dd["taskAgvSerialNo"].ToString().Trim(),
        //            taskStartQr = dd["taskStartQr"].ToString().Trim(),
        //            taskStartX = dd["taskStartX"].ToString().Trim(),
        //            taskStartY = dd["taskStartY"].ToString().Trim(),
        //            taskEndQr = dd["taskEndQr"].ToString().Trim(),
        //            taskEndX = dd["taskEndX"].ToString().Trim(),
        //            taskEndY = dd["taskEndY"].ToString().Trim(),
        //            taskComplete = int.Parse(dd["taskComplete"].ToString().Trim()),

        //            taskEndTime = dd["taskEndTime"].ToString().Trim(),
        //        });
        //    }
        //    dd.Close();
        //    var s = "SELECT COUNT(*) FROM `ga_taskloginfo`";
        //    DataTable f = MySqlHelper.ExecuteDataTable(s);
        //    foreach (DataRow item in f.Rows)
        //    {
        //        pageCount = int.Parse(item[0].ToString().Trim());
        //    }
        //    return list;
        //}
    }
}

