using Commonality.instrument;
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
        /// 查询货架数据信息
        /// </summary>
        /// <param name="pageCount">数据总数</param>
        /// <param name="limit">页面大小</param>
        /// <param name="offset">当前页</param>
        /// <returns></returns>
        public List<Ga_rack> Ga_rackShow(ref int pageCount, int limit, int offset, string rackSerialNum, string rackStatus)
        {
            List<Ga_rack> list = new List<Ga_rack>();
            var sql = "SELECT * FROM `ga_agv`.`ga_rack` where 0=0";
            if (rackSerialNum != null && rackSerialNum != "")
            {
                sql += " and rackSerialNum=" + rackSerialNum + "";
            }
            if (rackStatus == "空闲")
            {
                sql += " and rackStatus=" + 1;
            }
            if (rackStatus != "空闲")
            {
                if (rackStatus == "任务锁定")
                {
                    sql += " and rackStatus=" + 2;
                }
                else if (rackStatus == "移动中")
                {
                    sql += " and rackStatus=" + 3;
                }
                else if (rackStatus == "弃用")
                {
                    sql += " and rackStatus=" + 4;
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
            string count = sql.Replace("*", "Count(*)");
            count = count.Replace("LIMIT", " # ");

            MySqlDataReader mySql = MySqlHelper.ExecuteReader(count);
            while (mySql.Read())
            {
                pageCount = Convert.ToInt32(mySql[0].ToString().Trim());
                break;
            }
            return list;
        }
        /// <summary>
        /// 添加货架
        /// </summary>
        /// <param name="rack"></param>
        /// <returns></returns>
        public bool Addrack(Ga_rack rack)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO `ga_agv`.`ga_rack`(`rackSerialNum`, `rack_qrInfo`, `rackStatus`, `rack_agvSerailNum`) VALUES ( @rackSerialNum,@rack_qrInfo, @rackStatus, @rack_agvSerailNum)");
            MySqlParameter[] par ={
                        new MySqlParameter("@rackSerialNum",MySqlDbType.VarChar,10000){  Value=rack.rackSerialNum },
                        new MySqlParameter("@rack_qrInfo",MySqlDbType.VarChar,10000){  Value=rack.rack_qrInfo },
                        new MySqlParameter("@rackStatus",MySqlDbType.Int32,10000){  Value=rack.rackStatus },
                        new MySqlParameter("@rack_agvSerailNum",MySqlDbType.Int32,10000){  Value=rack.rack_agvSerailNum }
            };
            return MySqlHelper.ExecuteNonQuery(sql.ToString(), par) > 0 ? true : false;
        }


        /// <summary>
        /// 删除货架
        /// </summary>
        /// <param name="rack"></param>
        /// <returns></returns>
        public bool rackdelete(Ga_rack rack)
        {
            return MySqlHelper.ExecuteNonQuery("DELETE  FROM `ga_agv`.`ga_rack` WHERE rackId=@rackId", new MySqlParameter[] { (new MySqlParameter("@rackId", MySqlDbType.Int32, 1000) { Value = rack.rackId }) }) > 0 ? true : false;
        }


        /// <summary>
        /// 货架批量删除
        /// </summary>
        /// <param name="rack"></param>
        /// <returns></returns>
        public bool rackdeletelist(List<Ga_rack> rack)
        {
            List<string> sql = new List<string>();
            foreach (Ga_rack item in rack)
            {
                sql.Add("DELETE  FROM `ga_agv`.`ga_rack` WHERE rackId=" + item.rackId + ";");
            }
            return MySqlHelper.ExecuteSqlTran(sql);
        }

        /// <summary>
        /// 编辑货架
        /// </summary>
        /// <param name="rack"></param>
        /// <returns></returns>
        public bool editrack(Ga_rack rack)
        {
            return MySqlHelper.ExecuteNonQuery("UPDATE `ga_agv`.`ga_rack` SET rackSerialNum=@rackSerialNum,rack_qrInfo=@rack_qrInfo,rackStatus=@rackStatus,rack_agvSerailNum =@rack_agvSerailNum WHERE rackId=@rackId", new MySqlParameter[] {
                new MySqlParameter("@rackSerialNum",MySqlDbType.VarChar,10000){  Value=rack.rackSerialNum },
                        new MySqlParameter("@rack_qrInfo",MySqlDbType.VarChar,10000){  Value=rack.rack_qrInfo },
                        new MySqlParameter("@rackStatus",MySqlDbType.Int32,10000){  Value=rack.rackStatus },
                        new MySqlParameter("@rack_agvSerailNum",MySqlDbType.Int32,10000){  Value=rack.rack_agvSerailNum },
                        new MySqlParameter("@rackId",MySqlDbType.Int32,1000){Value=rack.rackId }
            }) > 0 ? true : false;
        }
    }
}