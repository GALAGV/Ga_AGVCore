using Ga_AGV.BLL;
using Ga_AGV.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ga_AGV.Core.Controllers
{
    public class ConfigurationController : Controller
    {
        Ga_settingBLL ga_Setting = new Ga_settingBLL();

        /// <summary>
        /// 系统配置
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Index()
        {
            List<Ga_setting> ga_s = ga_Setting.Ga_Settings();
            return View(ga_s);
        }
    }
}