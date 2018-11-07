using Ga_AGV.Model.DataModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.DAL.DataAccess
{
    public class Ga_qrcodeDAL
    {
        #region 数据处理

        #region 查询二维码管理

        /// <summary>
        /// 查询二维码管理
        /// </summary>
        /// <param name="PageCount">数据总数</param>
        /// <param name="limit">页面大小</param>
        /// <param name="offset">当前页</param>
        /// <returns></returns>
        public List<Ga_qrcode> Ga_qrcodesList(ref int PageCount, int limit, int offset)
        {
            List<Ga_qrcode> ga_s = new List<Ga_qrcode>();
            MySqlDataReader mySqlData = MySqlHelper.ExecuteReader("SELECT * FROM `ga_agv`.`ga_qrcode` LIMIT " + offset + "," + limit + "");
            while (mySqlData.Read())
            {
                ga_s.Add(new Ga_qrcode()
                {
                    qrId = Convert.ToInt32(mySqlData["qrId"].ToString().Trim()),
                    qrInfo = mySqlData["qrInfo"].ToString().Trim(),
                    qrX = Convert.ToInt32(mySqlData["qrX"].ToString().Trim()),
                    qrY = Convert.ToInt32(mySqlData["qrY"].ToString().Trim()),
                    qrStatus = mySqlData["qrStatus"].ToString().Trim(),
                    qrRemark = mySqlData["qrRemark"].ToString().Trim(),
                });
            }
            mySqlData.Close();
            MySqlDataReader mySql = MySqlHelper.ExecuteReader("SELECT Count(*) FROM `ga_agv`.`Ga_qrcode`");
            while (mySql.Read())
            {
                PageCount = Convert.ToInt32(mySql[0].ToString().Trim());
                break;
            }
            mySql.Close();
            return ga_s;
        }

        #endregion 查询二维码管理

        #endregion 数据处理
    }
}