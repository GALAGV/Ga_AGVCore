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
        public List<Ga_qrcode> DALShowQRplace(ref int mapX, ref int mapY)
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
            };
            mySqlData.Close();

            string map = "SELECT `widgetLong`,`widgetHeight` FROM `ga_agv`.`ga_widget` where widgetId = 1";

            MySqlDataReader mySql = MySqlHelper.ExecuteReader(map);
            while (mySql.Read())
            {
                mapX = Convert.ToInt32(mySql["widgetLong"].ToString().Trim());
                mapY = Convert.ToInt32(mySql["widgetHeight"].ToString().Trim());
                break;
            }
            mySql.Close();
            return ga_q;
        }
    }
}