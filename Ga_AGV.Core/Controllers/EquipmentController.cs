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
        [Authorize]
        public ActionResult AGVManagement()
        {
            //if (Request.ServerVariables["HTTP_REFERER"] == null)
            //{
            //    return RedirectToAction("Login", "Home");
            //}
            //else
            //{
                return View();
            //}
        }
    }
}