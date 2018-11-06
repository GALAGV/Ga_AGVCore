using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ga_AGV.Core.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Statistics

        public ActionResult RunningLog()
        {
            return View();
        }

        public ActionResult TaskLog()
        {
            return View();
        }

        /// <summary>
        /// AGV状态
        /// </summary>
        /// <returns></returns>
        public ActionResult AGVState()
        {
            return View();
        }
    }
}