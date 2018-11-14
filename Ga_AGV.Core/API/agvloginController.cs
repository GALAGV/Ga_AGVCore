using Ga_AGV.BLL;
using Ga_AGV.Model.Commonality;
using Ga_AGV.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using Commonality.instrument;

namespace Ga_AGV.Core.API
{
    /// <summary>
    /// 用户登录API
    /// </summary>
    public class agvloginController : ApiController
    {
        Ga_agvloginBLL Ga_AgvloginBLL = new Ga_agvloginBLL();

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="UserName">用户账号</param>
        /// <param name="Password">用户密码</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult LoginUser([FromBody] Ga_user User)
        {
            var Context = HttpContext.Current;
            Ga_user user = Ga_AgvloginBLL.UserLogin(User.userName, Md5.Encrypt(User.userPassword));
            //Ga_user user = Ga_AgvloginBLL.UserLogin(User.userName,User.userPassword);
            if (user != null)
            {
                Context.Session["User"] = user;
                FormsAuthentication.SetAuthCookie(user.userId.ToString(), false); //授权
                return new JsonResult() { Success = true, Message = "登录成功" };
            }
            else
            {
                return new JsonResult() { Success = false, Message = "账号或密码不正确！" };
            }
        }
        /// <summary>
        /// 判断密码是否正确
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UserPass([FromBody] Ga_user User)
        {
            var Context = HttpContext.Current;
            Ga_user user = Ga_AgvloginBLL.UserPass(Md5.Encrypt(User.userPassword));
            //Ga_user user = Ga_AgvloginBLL.UserLogin(User.userName,User.userPassword);
            if (user != null)
            {
                Context.Session["User"] = user;
                FormsAuthentication.SetAuthCookie(user.userId.ToString(), false); //授权
                return new JsonResult() { Success = true, Message = "密码正确" };
            }
            else
            {
                return new JsonResult() { Success = false, Message = "密码不正确！" };
            }
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="Rack"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Password([FromBody] Ga_user pass)
        {
            var Context = HttpContext.Current;
            Ga_user user = Context.Session["User"] as Ga_user;
            //Ga_user user = Ga_AgvloginBLL.UserPass(Md5.Encrypt(pass.userPassword));
            //= user;
            user.userPassword = Md5.Encrypt(pass.userPassword);
            if (Ga_AgvloginBLL.edit(user))
            {
                return new JsonResult() { Message = "修改成功", Success = true };
            }
            else
            {
                return new JsonResult() { Message = "修改失败", Success = false };
            }
        }
        /// <summary>
        /// 退出系统
        /// </summary>
        [HttpGet]
        public void exit()
        {
            FormsAuthentication.SignOut(); //取消授权
            HttpContext.Current.Response.Redirect("/Home/Login");
        }
    }
}
