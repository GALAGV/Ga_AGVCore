using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ga_AGV.Core.Controllers
{
    public class HomeController : Controller
    {

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Main()
        {
            return View();
        }



    }
}