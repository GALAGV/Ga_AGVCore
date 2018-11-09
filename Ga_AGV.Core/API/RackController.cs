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
    public class RackController : ApiController
    {
        Ga_rackBLL ga_RackBLL = new Ga_rackBLL();

        /// <summary>
        /// 查询货架
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonData<Ga_rack> Rack(int limit, int offset, string rackSerialNum, string rackStatus)
        {
            int pageCount = 0;
            JsonData<Ga_rack> list = new JsonData<Ga_rack>();
            list.rows = ga_RackBLL.Ga_rackList(ref pageCount, limit, offset, rackSerialNum, rackStatus);
            list.total = pageCount;
            return list;
        }
        /// <summary>
        /// 添加货架
        /// </summary>
        /// <param name="Rack"></param>
        /// <returns></returns>
        public JsonResult Rackadd([FromBody] Ga_rack Rackadd)
        {
            if (ga_RackBLL.rackadd(Rackadd)) {
                return new JsonResult() { Message = "添加成功", Success = true };
            }
            else
            {
                return new JsonResult() { Message = "添加失败", Success = false };
            }
        }
        /// <summary>
        /// 编辑货架
        /// </summary>
        /// <param name="Rack"></param>
        /// <returns></returns>
        public JsonResult editrack([FromBody] Ga_rack rackdata)
        {
            if (ga_RackBLL.edit(rackdata))
            {
                return new JsonResult() { Message = "修改成功", Success = true };
            }
            else
            {
                return new JsonResult() { Message = "修改失败", Success = false };
            }
        }

        /// <summary>
        /// 删除货架
        /// </summary>
        /// <param name="agvdata"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult deleterack([FromBody] Ga_rack rackdata)
        {
            if (ga_RackBLL.delete(rackdata))
            {
                return new JsonResult() { Message = "删除成功", Success = true };
            }
            else
            {
                return new JsonResult() { Message = "删除失败", Success = false };
            }
        }

        /// <summary>
        /// 删除货架（多条）
        /// </summary>
        /// <param name="agvdata"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult deletelist([FromBody] List<Ga_rack>rackdata)
        {
            if (ga_RackBLL.deletelist(rackdata))
            {
                return new JsonResult() { Message = "删除成功", Success = true };
            }
            else
            {
                return new JsonResult() { Message = "删除失败", Success = false };
            }
        }
    }
}
