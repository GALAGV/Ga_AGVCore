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
                return new Ga_user() { userName = Userdata.Rows[0]["userName"].ToString(), userPassword = Userdata.Rows[0]["userPassword"].ToString(), userAuth = Convert.ToInt32(Userdata.Rows[0]["userAuth"].ToString()) };
            }
            else
            {
                return null;
            }
        }
    }
}
