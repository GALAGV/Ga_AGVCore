using Ga_AGV.Model.DataModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.DAL.DataAccess
{
    public class Ga_settingDAL
    {
        /// <summary>
        /// 查询所有配置项
        /// </summary>
        /// <returns></returns>
        public List<Ga_setting> GetagvList()
        {
            List<Ga_setting> list = new List<Ga_setting>();
            MySqlDataReader dd = MySqlHelper.ExecuteReader("SELECT * FROM `ga_agv`.`ga_setting`");
            while (dd.Read())
            {
                list.Add(new Ga_setting()
                {
                    settingItem = dd["settingItem"].ToString().Trim(),
                    settingVlaue = dd["settingVlaue"].ToString().Trim(),
                });
            }
            dd.Close();
            return list;
        }

        /// <summary>
        /// 更新配置项
        /// </summary>
        /// <param name="ga_s"></param>
        /// <returns></returns>
        public bool Update(List<Ga_setting> ga_s)
        {
            List<string> sql = new List<string>();
            foreach (Ga_setting item in ga_s)
            {
                sql.Add("Update `ga_agv`.`ga_setting` SET settingVlaue='"+item.settingVlaue+ "' WHERE settingItem='"+item.settingItem+"'");
            }
            return MySqlHelper.ExecuteSqlTran(sql);
        }
    }
}
