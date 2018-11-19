using Ga_AGV.BLL;
using Ga_AGV.Model.Commonality;
using Ga_AGV.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ga_AGV.Core.API
{
    public class SettingController : ApiController
    {

        Ga_settingBLL ga_Setting = new Ga_settingBLL();


        /// <summary>
        /// 更新服务器配置项
        /// </summary>
        /// <param name="rackdata"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateSetting([FromBody] List<Ga_setting>  gas)
        {
            if (ga_Setting.Updatesetting(gas))
            {
                return new JsonResult() { Message = "保存成功", Success = true };
            }
            else
            {
                return new JsonResult() { Message = "保存失败", Success = false };
            }
        }




    }
}
