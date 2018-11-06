using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.Model.Commonality
{
    /// <summary>
    /// bootstrap数据对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonData<T> where T : class
    {
        public int total { get; set; } //数据总数

        public List<T> rows { get; set; } //结果集
    }
}
