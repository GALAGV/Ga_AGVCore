using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ga_AGV.Core.Controllers
{
    public class EquipmentController : Controller
    {
        // GET: Equipment

        /// <summary>
        /// /AGV管理
        /// </summary>
        /// <returns></returns>
        public ActionResult AGVManagement()
        {
            return View();
        }


    }
}