using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.Model.Commonality
{
    public class SocketData
    {
        public string Address { get; set; }

        public int Port { get; set; }

        public int maxConnect { get; set; }

        public bool IsRunning { get; set; }

        public int OnClientCount { get; set; }
    }
}
