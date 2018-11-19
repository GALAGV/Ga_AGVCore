using Ga_AGV.DAL.DataAccess;
using Ga_AGV.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.BLL
{
    public class Ga_settingBLL
    {
        Ga_settingDAL ga_SettingDAL = new Ga_settingDAL();

        /// <summary>
        /// 查询配置项
        /// </summary>
        /// <returns></returns>
        public List<Ga_setting> Ga_Settings()
        {
            return ga_SettingDAL.GetagvList();
        }

        /// <summary>
        /// 更新服务配置项
        /// </summary>
        /// <param name="ga_s"></param>
        /// <returns></returns>
        public bool Updatesetting(List<Ga_setting> ga_s)
        {
            return ga_SettingDAL.Update(ga_s);
        }
    }
}
