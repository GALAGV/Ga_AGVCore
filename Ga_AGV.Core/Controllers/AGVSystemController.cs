using Ga_AGV.BLL;
using Ga_AGV.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ga_AGV.Core.Controllers
{
    public class AGVSystemController : Controller
    {
        private Ga_settingBLL ga_Setting = new Ga_settingBLL();

        // GET: AGVSystem
        [Authorize]
        public ActionResult QR_Code()
        {
            return View();
        }

        public ActionResult AGVMonitoring()
        {
            return View();
        }
    }
}