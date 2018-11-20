using Ga_AGV.BLL;
using Ga_AGV.Model.Commonality;
using Ga_AGV.Model.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Threading;

namespace Ga_AGV.Core.API
{
    public class FileController : ApiController
    {
        Ga_agvlogBLL agvlogBLL = new Ga_agvlogBLL();

        /// <summary>
        /// 下载日志
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonFile Filelog([FromBody] Ga_agvloginfo ga)
        {
            try
            {
                string Time= (ga.agvLogTime.Equals("") ? DateTime.Now.ToString("yyyyMMdd") : Convert.ToDateTime(ga.agvLogTime).ToString("yyyyMMdd"));
                string LogTableName = "ga_agvloginfo" + Time;
                string TaskLogName = "ga_taskloginfo" + Time;
                if (agvlogBLL.TableExistx(LogTableName))
                {
                    if (Directory.Exists(HttpContext.Current.Server.MapPath("~/Log")) == true)
                    {
                        Directory.Delete(HttpContext.Current.Server.MapPath("~/Log"), true);
                        Thread.Sleep(500);
                    }
                    if (Directory.Exists(HttpContext.Current.Server.MapPath("~/Log")) == false)
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Log"));
                    }
                    string sqlText = agvlogBLL.log(LogTableName, "ga_agvlog");
                    if (agvlogBLL.TableExistx(TaskLogName))
                    {
                        sqlText += agvlogBLL.log(TaskLogName, "ga_agvlog");
                    }
                    string fileNames = Time + "(log)" + ".zip";
                    byte[] bytes = System.Text.Encoding.Default.GetBytes(sqlText);//把字符串转成byte数组
                    using (FileStream outfile = new FileStream(HttpContext.Current.Server.MapPath("~/Log/" + fileNames + ""), FileMode.Create))
                    {
                        using (GZipStream zipStream = new GZipStream(outfile, CompressionMode.Compress))
                        {
                            zipStream.Write(bytes, 0, bytes.Length);
                        }
                    }
                    return new JsonFile() { Success = true, Data = fileNames, Message = "导出成功" };
                }
                else
                {
                    return new JsonFile() { Success = false, Message = "暂无日志！" };
                }
            }
            catch (Exception ex)
            {
                return new JsonFile() { Success = false, Message = "下载失败,错误信息:" + ex.Message };
            }
        }
    }
}
