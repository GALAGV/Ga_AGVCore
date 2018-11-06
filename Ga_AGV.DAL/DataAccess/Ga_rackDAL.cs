using Ga_AGV.Model.DataModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.DAL.DataAccess
{
    public class Ga_rackDAL
    {
        /// <summary>
        /// 查询QR_Code数据信息
        /// </summary>
        /// <param name="pageCount">数据总数</param>
        /// <param name="limit">页面大小</param>
        /// <param name="offset">当前页</param>
        /// <returns></returns>
        public List<Ga_rack> Ga_rackShow(ref int pageCount, int limit, int offset)
        {
            List<Ga_rack> list = new List<Ga_rack>();
            var sql = "SELECT * FROM  `ga_agv`.`ga_rack` LIMIT " + offset + "," + limit + "";
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