using Ga_AGV.Model.DataModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commonality.instrument;

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

        /// <summary>
        /// 添加AGV
        /// </summary>
        /// <param name="agv"></param>
        /// <returns></returns>
        public bool Addagv(Ga_agv agv)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO `ga_agv`.`ga_agv`(`agvNum`, `agvSerialNum`, `agvName`, `agvIp`, `agvPort`, `agvCreateTime`, `agvOffLineTime`, `agvOnLineTime`, `agvFirmware`) VALUES ( @agvNum,@agvSerialNum, @agvName, @agvIp, @agvPort, @agvCreateTime, @agvOffLineTime, @agvOnLineTime,@agvFirmware)");
            MySqlParameter[] par ={
                        new MySqlParameter("@agvNum",MySqlDbType.Int32,10000){  Value=agv.agvNum },
                        new MySqlParameter("@agvSerialNum",MySqlDbType.VarChar,10000){  Value=agv.agvSerialNum },
                        new MySqlParameter("@agvName",MySqlDbType.VarChar,10000){  Value=agv.agvName },
                        new MySqlParameter("@agvIp",MySqlDbType.VarChar,10000){  Value=agv.agvIp },
                        new MySqlParameter("@agvPort",MySqlDbType.Int32,10000){  Value=agv.agvPort },
                        new MySqlParameter("@agvCreateTime",MySqlDbType.Int32,10000){  Value=UTC.ConvertDateTimeLong(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))) },
                        new MySqlParameter("@agvOffLineTime",MySqlDbType.VarChar,10000){  Value=agv.agvOffLineTime },
                        new MySqlParameter("@agvOnLineTime",MySqlDbType.VarChar,10000){  Value=agv.agvOnLineTime },
                        new MySqlParameter("@agvFirmware",MySqlDbType.VarChar,10000){  Value=agv.agvFirmware },
            };
            return MySqlHelper.ExecuteNonQuery(sql.ToString(), par) > 0 ? true : false;
        }
    }
}
