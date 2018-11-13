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
        [Authorize]
        public ActionResult RunningLog()
        {
            return View();
        }

        [Authorize]
        public ActionResult TaskLog()
        {
            return View();
        }
        [Authorize]
        public ActionResult Rack()
        {
            return View();
        }

        /// <summary>
        /// AGV状态
        /// </summary>
        /// <returns></returns>
         [Authorize]
        public ActionResult AGVState()
        {
            return View();
        }
        [Authorize]
        public ActionResult RunMonitoring()
        {
            return View();
        }
    }
}