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
    /// <summary>
    /// AGV管理API
    /// </summary>
    public class agvlistController : ApiController
    {
        Ga_agvBLL Ga_Agv = new Ga_agvBLL();

        /// <summary>
        /// 获取所有AGV数据
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonData<Ga_agv> agv(int limit, int offset, string agvNum)
        {
            int pageCount = 0;
            JsonData<Ga_agv> data = new JsonData<Ga_agv>();
            data.rows = Ga_Agv.GetagvData(ref pageCount, limit, offset, agvNum == "" ? 0 : Convert.ToInt32(agvNum));
            data.total = pageCount;
            return data;
        }

        /// <summary>
        /// 添加AGV
        /// </summary>
        /// <param name="agvdata"></param>
        /// <returns></returns>
        public JsonResult Addagv([FromBody] Ga_agv agvdata)
        {
            if (Ga_Agv.agvadd(agvdata))
            {
                return new JsonResult() { Message = "添加成功", Success = true };
            }
            else
            {
                return new JsonResult() { Message = "添加失败", Success = false };
            }
        }

        /// <summary>
        /// 编辑AGV
        /// </summary>
        /// <param name="agvdata"></param>
        /// <returns></returns>
        public JsonResult editagv([FromBody] Ga_agv agvdata)
        {
            if (Ga_Agv.edit(agvdata))
            {
                return new JsonResult() { Message = "修改成功", Success = true };
            }
            else
            {
                return new JsonResult() { Message = "修改失败", Success = false };
            }
        }

        /// <summary>
        /// 删除agv
        /// </summary>
        /// <param name="agvdata"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult deleteagv([FromBody] Ga_agv agvdata)
        {
            if (Ga_Agv.delete(agvdata))
            {
                return new JsonResult() { Message = "删除成功", Success = true };
            }
            else
            {
                return new JsonResult() { Message = "删除失败", Success = false };
            }
        }

        /// <summary>
        /// 删除AGV（多条）
        /// </summary>
        /// <param name="agvdata"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult deletelist([FromBody] List<Ga_agv> agvdata)
        {
            if (Ga_Agv.deletelist(agvdata))
            {
                return new JsonResult() { Message = "删除成功", Success = true };
            }
            else
            {
                return new JsonResult() { Message = "删除失败", Success = false };
            }
        }

        /// <summary>
        /// 启动AGV
        /// </summary>
        /// <param name="agvdata"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult agvstart([FromBody] Ga_agv agvdata)
        {
            return new JsonResult() { Message = "模拟测试" };
        }

        /// <summary>
        /// 停止AGV
        /// </summary>
        /// <param name="agvdata"></param>
        /// <returns></returns>
        public JsonResult agvstop([FromBody] Ga_agv agvdata)
        {
            return new JsonResult() { Message = "模拟测试" };
        }

        /// <summary>
        /// 云台上升
        /// </summary>
        /// <param name="agvdata"></param>
        /// <returns></returns>
        public JsonResult DeckRise([FromBody] Ga_agv agvdata)
        {
            return new JsonResult() { Message = "模拟测试" };
        }

        /// <summary>
        /// 云台下降
        /// </summary>
        /// <param name="agvdata"></param>
        /// <returns></returns>
        public JsonResult DeckDecline([FromBody] Ga_agv agvdata)
        {
            return new JsonResult() { Message = "模拟测试" };
        }

        /// <summary>
        /// 左转
        /// </summary>
        /// <param name="agvdata"></param>
        /// <returns></returns>
        public JsonResult LeftTurn([FromBody] Ga_agv agvdata)
        {
            return new JsonResult() { Message = "模拟测试" };
        }

        /// <summary>
        /// 右转
        /// </summary>
        /// <param name="agvdata"></param>
        /// <returns></returns>
        public JsonResult rightTurn([FromBody] Ga_agv agvdata)
        {
            return new JsonResult() { Message = "模拟测试" };
        }

        /// <summary>
        /// 更改速度
        /// </summary>
        /// <param name="agvdata"></param>
        /// <returns></returns>
        public JsonResult UpdateSpeed([FromBody] Json<Ga_agv> agvdata)
        {
            return new JsonResult() { Message = "模拟测试" };
        }

        /// <summary>
        /// 更改PBS
        /// </summary>
        /// <param name="agvdata"></param>
        /// <returns></returns>
        public JsonResult UpdatePBS([FromBody] Json<Ga_agv> agvdata)
        {
            return new JsonResult() { Message = "模拟测试" };
        }

        /// <summary>
        /// 急停
        /// </summary>
        /// <param name="agvdata"></param>
        /// <returns></returns>
        public JsonResult Scram([FromBody] Ga_agv agvdata)
        {
            return new JsonResult() { Message = "模拟测试" };
        }

        /// <summary>
        /// 手动自动
        /// </summary>
        /// <param name="agvdata"></param>
        /// <returns></returns>
        public JsonResult ManualSelfMotion([FromBody] Ga_agv agvdata)
        {
            return new JsonResult() { Message = "模拟测试" };
        }

        /// <summary>
        /// 复位
        /// </summary>
        /// <param name="agvdata"></param>
        /// <returns></returns>
        public JsonResult Restoration([FromBody] Ga_agv agvdata)
        {
            return new JsonResult() { Message="模拟测试" };
        }

        static int a = 0;
        /// <summary>
        /// AGV状态回读
        /// </summary>
        /// <returns></returns>
        public JsonagvInfo agvState([FromBody] Ga_agv agvdata)
        {

            if (a > 10)
            {
                a = 0;
            }
            a++;
            return new JsonagvInfo() { Success = true, agvNum = 1, agvFirmware = "V1.02.3" + a, agvHolder = a > 5 ? "上升" : "下降", agvIsRunning = a > 5 ? "离线" : "在线", Message = a > 5 ? "正常" : "障碍物检测中", qrcode = "003", PBS = "区域" + a, agvspeed = a * 2, voltage = a * 6 };
        }





    }
}
