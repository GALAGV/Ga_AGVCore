using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace Ga_AGV.Core
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Task.Factory.StartNew(CyC);
        }

        //[DllImport("kernel32.dll")]
        //private static extern long WritePrivateProfileString(string section, string key, string value, string filepath);

        //[DllImport("kernel32.dll")]
        //private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder returnvalue, int buffersize, string filepath);

        //public void CyC()
        //{
        //    string IniFilePath = "D:\\Config.ini";
        //    string Section = "cs";
        //    try
        //    {
        //        WritePrivateProfileString(Section, "startdate", DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"), IniFilePath);

        //        for (long i = 1; i < 100000; i++)
        //        {
        //            WritePrivateProfileString(Section, "date" + i, DateTime.Now.ToString("HH-mm-ss"), IniFilePath);
        //            Thread.Sleep(10000);
        //        }
        //        WritePrivateProfileString(Section, "escdate", DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"), IniFilePath);
        //    }
        //    catch (Exception)
        //    {
        //        WritePrivateProfileString(Section, "escdate", DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "异常退出", IniFilePath);
        //    }
        //}

        #region 开启WebApi Session支持

        public override void Init()
        {
            this.PostAuthenticateRequest += (sender, e) => HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            base.Init();
        }

        #endregion 开启WebApi Session支持
    }
}