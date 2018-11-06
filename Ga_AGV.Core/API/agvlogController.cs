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
    /// 运行日志API
    /// </summary>
    public class agvlogController : ApiController
    {
        private Ga_agvlogBLL ga_AgvlogBLL = new Ga_agvlogBLL();

        ///// <summary>
        ///// 获取日志
        ///// </summary>
        ///// <param name="limit">页面大小</param>
        ///// <param name="offset">当前页</param>
        ///// <returns></returns>
        //[HttpGet]
        //public JsonData<Ga_agvloginfo> Log(int limit, int offset)
        //{
        //    //    query_log_time: $("#query_log_time").val(),
        //    //    query_start_time: $("#query_start_time").val(),
        //    //    query_end_time: $("#query_end_time").val(),
        //    //    query_agv_num: $("#query_agv_num").val(),
        //    //    query_task_status: $("#query_task_status").val(),
        //    //    query_agv_status: $("#query_agv_status").val(),

        //    int PageCount = 0;
        //    JsonData<Ga_agvloginfo> data = new JsonData<Ga_agvloginfo>();
        //    data.rows = ga_AgvlogBLL.Ga_AgvloginfosBLL(ref PageCount, limit, offset);
        //    data.total = PageCount;
        //    return data;
        //}

        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="limit">页面大小</param>
        /// <param name="offset">当前页</param>
        /// <returns></returns>
        [HttpGet]
        public JsonData<Ga_agvloginfo> Log(int limit, int offset, string query_log_time, string query_start_time, string query_end_time, string query_agv_num, string query_task_status, string query_agv_status)
        {
            //query_log_time: $("#query_log_time").val(),
            //    query_start_time: $("#query_start_time").val(),
            //    query_end_time: $("#query_end_time").val(),
            //    txt_search_rolename: $("#query_agv_num").val(),
            //    query_task_status: $("#query_task_status").val(),
            //    query_agv_status: $("#query_agv_status").val(),
            int PageCount = 0;
            JsonData<Ga_agvloginfo> data = new JsonData<Ga_agvloginfo>
            {
                rows = ga_AgvlogBLL.Ga_AgvloginfosBLL(ref PageCount, limit, offset, query_log_time, query_start_time, query_end_time, query_agv_num, query_task_status, query_agv_status),
                total = PageCount
            };
            return data;
        }
    }
}