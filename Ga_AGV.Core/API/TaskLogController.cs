using Ga_AGV.Model.Commonality;
using Ga_AGV.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ga_AGV.BLL;

namespace Ga_AGV.Core.API
{
    public class TaskLogController : ApiController
    {
        Ga_tasklogBLL ga_Tasklog = new Ga_tasklogBLL();
        /// <summary>
        /// 查询任务日志（查询）
        /// </summary>
        /// <param name="pageCount">数据总数</param>
        /// <param name="limit">页面大小</param>
        /// <param name="offset">当前页</param>
        /// <param name="AGVNum">AGV编号</param>
        /// <param name="Time">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <param name="Log_Time">日志时间</param>
        /// <param name="taskComplete">任务状态</param>
        /// <returns></returns>
        [HttpGet]
        public JsonData<Ga_taskloginfo> TaskLog(int limit, int offset,string AGVNum,string Time,string EndTime, string Log_Time, string taskComplete)
        {
            int pageCount = 0;
            JsonData<Ga_taskloginfo> list = new JsonData<Ga_taskloginfo>();
            list.rows = ga_Tasklog.TaskLoglist(ref pageCount, limit, offset,AGVNum,Time,EndTime, Log_Time, taskComplete);
            list.total = pageCount;
            return list;
        }
    }
}
