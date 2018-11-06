using Ga_AGV.DAL.DataAccess;
using Ga_AGV.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.BLL
{
    public class Ga_tasklogBLL
    {
        Ga_tasklogDAl ga_TasklogDAl = new Ga_tasklogDAl();
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
        public List<Ga_taskloginfo> TaskLoglist(ref int pageCount, int limit, int offset,string AGVNum,string Time,string EndTime,string LogTime,string taskComplete)
        {
            return ga_TasklogDAl.Ga_taskloglist(ref pageCount, limit, offset, AGVNum, Time, EndTime,LogTime, taskComplete);
        }
    }
}
