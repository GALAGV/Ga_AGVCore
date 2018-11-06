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
    public class Ga_agvDAL
    {
        /// <summary>
        /// 查询所有AGV
        /// </summary>
        /// <param name="pageCount">数据总数</param>
        /// <param name="limit">页面大小</param>
        /// <param name="offset">当前页</param>
        /// <returns></returns>
        public List<Ga_agv> GetagvList(ref int pageCount, int limit, int offset)
        {
            List<Ga_agv> list = new List<Ga_agv>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM `ga_agv`.`ga_agv` LIMIT @offset,@limit ");
            MySqlParameter[] par ={
                        new MySqlParameter("@offset",MySqlDbType.Int32,10000),
                        new MySqlParameter("@limit",MySqlDbType.Int32,10000),
            };
            par[0].Value = offset;
            par[1].Value = limit;
            MySqlDataReader dd = MySqlHelper.ExecuteReader(sql.ToString(), par);
            while (dd.Read())
            {
                list.Add(new Ga_agv()
                {
                    agvId = Convert.ToInt32(dd["agvId"].ToString().Trim()),
                    agvNum = Convert.ToInt32(dd["agvNum"].ToString().Trim()),
                    agvSerialNum = dd["agvSerialNum"].ToString().Trim(),
                    agvName = dd["agvName"].ToString().Trim(),
                    agvIp = dd["agvIp"].ToString().Trim(),
                    agvPort = Convert.ToInt32(dd["agvPort"].ToString().Trim()),
                    agvCreateTime = long.Parse(dd["agvCreateTime"].ToString().Trim()),
                    agvOffLineTime = dd["agvOffLineTime"].ToString().Trim(),
                    agvOnLineTime = dd["agvOnLineTime"].ToString().Trim(),
                    agvFirmware = dd["agvFirmware"].ToString().Trim(),
                });
            }
            dd.Close();
            var s = "SELECT COUNT(*) FROM `ga_agv`.`ga_agv`";
            DataTable f = MySqlHelper.ExecuteDataTable(s);
            foreach (DataRow item in f.Rows)
            {
                pageCount = int.Parse(item[0].ToString().Trim());
            }
            return list;
        }
    }
}
