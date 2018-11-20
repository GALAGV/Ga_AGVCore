using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.Model.DataModel
{
    public class Ga_map
    {
        public string Map_name { get; set; }
        public int Map_x { get; set; }
        public int Qr_x { get; set; }
        public int Map_y { get; set; }
        public int Qr_y { get; set; }
        public string Widget_info { get; set; }
    }
}