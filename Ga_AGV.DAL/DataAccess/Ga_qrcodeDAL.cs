using Ga_AGV.Model.DataModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.DAL.DataAccess
{
    public class Ga_qrcodeDAL
    {
        #region 数据处理

        #region 增删查改 二维码管理

        /// <summary>
        /// 查询二维码管理
        /// </summary>
        /// <param name="PageCount">数据总数</param>
        /// <param name="limit">页面大小</param>
        /// <param name="offset">当前页</param>
        /// <returns></returns>
        public List<Ga_qrcode> Ga_qrcodesList(ref int PageCount, int limit, int offset, string qrId, int qrStatus)
        {
            List<Ga_qrcode> ga_s = new List<Ga_qrcode>();
            string sql = "SELECT * FROM `ga_agv`.`ga_qrcode` WHERE 0 = 0 ";

            if (qrId != null)
            {
                sql += " AND qrId = " + qrId + " ";
            }
            if (qrStatus != 0)
            {
                sql += " AND qrStatus = " + qrStatus + " ";
            }
            sql += " LIMIT " + offset + "," + limit + "";

            MySqlDataReader mySqlData = MySqlHelper.ExecuteReader(sql);

            while (mySqlData.Read())
            {
                ga_s.Add(new Ga_qrcode()
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

        public bool Ga_AddQRcode(Ga_qrcode qr)
        {
            StringBuilder SQLString = new StringBuilder();
            SQLString.Append("INSERT INTO `ga_agv`.`ga_qrcode`(`qrInfo`, `qrX`, `qrY`, `qrStatus`, `qrRemark`) VALUES (@qrInfo, @qrX, @qrY, @qrStatus, @qrRemark)");
            MySqlParameter[] cmdParms ={
                        new MySqlParameter("@qrInfo",MySqlDbType.VarChar){ Value=qr.qrInfo },
                        new MySqlParameter("@qrX",MySqlDbType.Int32){ Value=qr.qrX },
                        new MySqlParameter("@qrY",MySqlDbType.Int32){ Value=qr.qrY },
                        new MySqlParameter("@qrStatus",MySqlDbType.Int32){ Value=qr.qrStatus },
                        new MySqlParameter("@qrRemark",MySqlDbType.VarChar){ Value=qr.qrRemark },
            };
            return MySqlHelper.ExecuteNonQuery(SQLString.ToString(), cmdParms) > 0 ? true : false;
        }

        public bool Ga_UpQRcode(Ga_qrcode qr)
        {
            StringBuilder SQLString = new StringBuilder();
            SQLString.Append("UPDATE `ga_agv`.`ga_qrcode` SET `qrInfo` = @qrInfo, `qrX` = @qrX, `qrY` = @qrY, `qrStatus` = @qrStatus, `qrRemark` = @qrRemark WHERE `qrId` = @qrId");
            MySqlParameter[] cmdParms ={
                        new MySqlParameter("@qrId",MySqlDbType.Int32){ Value=qr.qrId },
                        new MySqlParameter("@qrInfo",MySqlDbType.VarChar){ Value=qr.qrInfo },
                        new MySqlParameter("@qrX",MySqlDbType.Int32){ Value=qr.qrX },
                        new MySqlParameter("@qrY",MySqlDbType.Int32){ Value=qr.qrY },
                        new MySqlParameter("@qrStatus",MySqlDbType.Int32){ Value=qr.qrStatus },
                        new MySqlParameter("@qrRemark",MySqlDbType.VarChar){ Value=qr.qrRemark },
            };
            return MySqlHelper.ExecuteNonQuery(SQLString.ToString(), cmdParms) > 0 ? true : false;
        }

        public bool Ga_DelQRcode(List<Ga_qrcode> qr)
        {
            List<string> sql = new List<string>();
            foreach (Ga_qrcode item in qr)
            {
                sql.Add("DELETE FROM `ga_agv`.`ga_qrcode` WHERE `qrId` = " + item.qrId + "");
            }
            return MySqlHelper.ExecuteSqlTran(sql);
        }

        #endregion 增删查改 二维码管理

        /// <summary>
        /// 批量添加二维码 and 添加地图尺寸
        /// </summary>
        /// <param name="map"></param>
        /// <param name="qr"></param>
        /// <returns></returns>
        public bool Ga_AddsQRcode(string map_name, int map_x, int map_y, string widget_info, List<Ga_qrcode> qr)
        {
            //map_name: $("#map_name").val(),
            //map_length: $("#map_length").val(),
            //qr_length: $("#qr_length").val(),
            //map_width: $("#map_width").val(),
            //qr_width: $("#qr_width").val(),
            //widget_info: $("#widget_info").val()

            StringBuilder SQLString_map = new StringBuilder();
            SQLString_map.Append("UPDATE `ga_agv`.`ga_widget` SET `widgetName` = @widgetName, `widgetType` = 3 , `widgetLong` = @widgetLong , `widgetHeight` = @widgetHeight , `widgetInfo` = @widget_info WHERE `widgetId` = 1");
            MySqlParameter[] cmdParms_map ={
                        new MySqlParameter("@widgetName",MySqlDbType.VarChar){ Value=map_name },
                        new MySqlParameter("@widgetLong",MySqlDbType.Int32){ Value= map_x},
                        new MySqlParameter("@widgetHeight",MySqlDbType.Int32){ Value= map_y},
                        new MySqlParameter("@widget_info",MySqlDbType.VarChar){ Value= widget_info},
            };
            bool is_map = MySqlHelper.ExecuteNonQuery(SQLString_map.ToString(), cmdParms_map) > 0 ? true : false;

            List<string> sql = new List<string>();
            int i = 1;
            foreach (Ga_qrcode item in qr)
            {
                sql.Add("INSERT INTO `ga_agv`.`ga_qrcode`(`qrInfo`, `qrX`, `qrY`, `qrStatus`, `qrRemark`) VALUES (" + i + ", " + item.qrX + ", " + item.qrY + ", 1, " + i + ")");
                i++;
            }
            bool is_qr = MySqlHelper.ExecuteSqlTran(sql);

            if (is_map && is_qr)
                return true;
            return false;
        }

        #endregion 数据处理
    }
}