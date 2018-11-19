using Ga_AGV.Model.DataModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.DAL.DataAccess
{
    public class Ga_agvMonitoringDAL
    {
        public List<Ga_qrcode> DALShowQRplace(ref int PageCount)
        {
            List<Ga_qrcode> ga_q = new List<Ga_qrcode>();
            string sql = "SELECT * FROM `ga_agv`.`ga_qrcode`";

            MySqlDataReader mySqlData = MySqlHelper.ExecuteReader(sql);

            while (mySqlData.Read())
            {
                ga_q.Add(new Ga_qrcode()
                {
                    qrId = Convert.ToInt32(mySqlData["qrId"].ToString().Trim()),
                    qrInfo = mySqlData["qrInfo"].ToString().Trim(),
                    qrX = Convert.ToInt32(mySqlData["qrX"].ToString().Trim()),
                    qrY = Convert.ToInt32(mySqlData["qrY"].ToString().Trim()),
                    qrStatus = Convert.ToInt32(mySqlData["qrStatus"].ToString().Trim()),
                    qrRemark = mySqlData["qrRemark"].ToString().Trim(),
                });
            }
            mySqlData.Close();

            string count = sql.Replace("*", "Count(*)");

            MySqlDataReader mySql = MySqlHelper.ExecuteReader(count);
            while (mySql.Read())
            {
                PageCount = Convert.ToInt32(mySql[0].ToString().Trim());
                break;
            }
            mySql.Close();
            return ga_q;
        }
    }
}