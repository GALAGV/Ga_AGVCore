using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ga_AGV.Core.Controllers
{
    public class AGVSystemController : Controller
    {
        // GET: AGVSystem
        public ActionResult QRcode()
        {
            return View();
        }
    }
}