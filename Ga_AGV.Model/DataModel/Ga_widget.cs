using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.Model.DataModel
{
    /// <summary>
    /// 控件信息实体类
    /// </summary>
    public class Ga_widget
    {
        /// <summary>
        /// 控件ID
        /// </summary>
        public int widgetId { get; set; }

        /// <summary>
        /// 控件名称
        /// </summary>
        public string widgetName { get; set; }

        /// <summary>
        /// 控件类型 1.文字 2.图片
        /// </summary>
        public int widgetType { get; set; }

        /// <summary>
        /// X坐标
        /// </summary>
        public double widgetX { get; set; }

        /// <summary>
        /// Y坐标
        /// </summary>
        public double widgetY { get; set; }

        /// <summary>
        /// 控件长度
        /// </summary>
        public double widgetLong { get; set; }

        /// <summary>
        /// 控件宽度
        /// </summary>
        public double widgetHeight { get; set; }

        /// <summary>
        /// 控件文本
        /// </summary>
        public double widgetTxt { get; set; }

        /// <summary>
        /// 控件信息
        /// </summary>
        public double widgetInfo { get; set; }
    }
}
