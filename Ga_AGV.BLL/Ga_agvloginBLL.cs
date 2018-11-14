using Ga_AGV.DAL.DataAccess;
using Ga_AGV.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.BLL
{
    public class Ga_agvloginBLL
    {
        Ga_agvloginDAL Getlogin = new Ga_agvloginDAL();

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public Ga_user UserLogin(string UserName, string Password)
        {
            return Getlogin.Login(UserName, Password);
        }
        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public Ga_user UserPass( string Password)
        {
            return Getlogin.ga_User(Password);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="agv"></param>
        /// <returns></returns>
        public bool edit(Ga_user pass)
        {
            return Getlogin.Password(pass);
        }

    }
}
