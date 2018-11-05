using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ga_AGV.Model.DataModel;
using MySql.Data.MySqlClient;

namespace Ga_AGV.DAL.DataAccess
{
    public class Ga_agvlogDAL
    {
        #region  数据处理


        #region 查询运行日志

        /// <summary>
        /// 查询运行日志
        /// </summary>
        /// <param name="PageCount">数据总数</param>
        /// <param name="limit">页面大小</param>
        /// <param name="offset">当前页</param>
        /// <returns></returns>
        public List<Ga_agvloginfo> Ga_AgvloginfosList(ref int PageCount, int limit, int offset)
        {
            List<Ga_agvloginfo> ga_s = new List<Ga_agvloginfo>();
            MySqlDataReader mySqlData = MySqlHelper.ExecuteReader("SELECT * FROM `ga_agvlog`.`ga_agvloginfo20181105` LIMIT " + offset + ","+ limit + "");
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
            MySqlDataReader mySql = MySqlHelper.ExecuteReader("SELECT Count(*) FROM `ga_agvlog`.`ga_agvloginfo20181105`");
            while (mySql.Read())
            {
                PageCount = Convert.ToInt32(mySql[0].ToString().Trim());
                break;
            }
            mySql.Close();
            return ga_s;
        }
        #endregion



        #endregion
    }
}
