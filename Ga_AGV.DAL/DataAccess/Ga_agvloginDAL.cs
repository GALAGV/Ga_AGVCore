using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Ga_AGV.Model.DataModel;
using MySql.Data.MySqlClient;

namespace Ga_AGV.DAL.DataAccess
{
    public class Ga_agvloginDAL
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="UserName">账户</param>
        /// <param name="Password">密码</param>
        /// <returns></returns>
        public Ga_user Login(string UserName, string Password)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM `ga_agv`.`ga_user` WHERE userName=@userName and userPassword=@userPassword");
            MySqlParameter[] par ={
                        new MySqlParameter("@userName",MySqlDbType.VarChar,10000),
                        new MySqlParameter("@userPassword",MySqlDbType.VarChar,10000),
            };
            par[0].Value = UserName;
            par[1].Value = Password;
            DataTable Userdata = MySqlHelper.ExecuteDataTable(sql.ToString(), par);
            if (Userdata.Rows.Count > 0)
            {
                return new Ga_user() {userName = Userdata.Rows[0]["userName"].ToString(), userPassword = Userdata.Rows[0]["userPassword"].ToString(), userAuth = Convert.ToInt32(Userdata.Rows[0]["userAuth"].ToString()),userId=Convert.ToInt32(Userdata.Rows[0]["userId"].ToString())};
            }
            else
            {
                return null;
            }
        }
        public Ga_user ga_User(string Password)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM `ga_agv`.`ga_user` WHERE userPassword=@userPassword");
            MySqlParameter[] par ={
                        new MySqlParameter("@userPassword",MySqlDbType.VarChar,10000),
            };
            par[0].Value = Password;
            DataTable Userdata = MySqlHelper.ExecuteDataTable(sql.ToString(), par);
            if (Userdata.Rows.Count > 0)
            {
                return new Ga_user() { userName = Userdata.Rows[0]["userName"].ToString(), userPassword = Userdata.Rows[0]["userPassword"].ToString(), userAuth = Convert.ToInt32(Userdata.Rows[0]["userAuth"].ToString()), userId = Convert.ToInt32(Userdata.Rows[0]["userId"].ToString()) };
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="rack"></param>
        /// <returns></returns>
        public bool Password(Ga_user pass)
        {
            return MySqlHelper.ExecuteNonQuery("UPDATE `ga_agv`.`ga_user` SET userPassword=@userPassword WHERE userId=@userId", new MySqlParameter[] {
                new MySqlParameter("@userPassword",MySqlDbType.VarChar,10000){  Value=pass.userPassword },
                new MySqlParameter("@userId",MySqlDbType.Int32,1000){ Value=pass.userId},
            }) > 0 ? true : false;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        //public Ga_user Password(string pass,int )
        //{
        //    StringBuilder sql = new StringBuilder();
        //    sql.Append("UPDATE `ga_user` set userPassword=@userPassword where userId=@userId");
        //    MySqlParameter[] par =
        //    {
        //        new MySqlParameter("@userPassword",MySqlDbType.VarChar,10000),
        //         new MySqlParameter("@userId",MySqlDbType.Int32,10000),
        //    };
        //    par[0].Value = pass;
        //    par[1].Value=
        //    DataTable Userdata = MySqlHelper.ExecuteDataTable(sql.ToString(), par);
        //    if (Userdata.Rows.Count > 0)
        //    {
        //        return new Ga_user() { userName = Userdata.Rows[0]["userName"].ToString(), userPassword = Userdata.Rows[0]["userPassword"].ToString(), userAuth = Convert.ToInt32(Userdata.Rows[0]["userAuth"].ToString()), userId = Convert.ToInt32(Userdata.Rows[0]["userId"].ToString()) };
        //    }
        //    return null;
        //}
    }
}
