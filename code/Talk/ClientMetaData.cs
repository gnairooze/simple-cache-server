using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talk
{
    internal class ClientMetaData
    {
        public string IpAddress { get; set; } = "not set";
        public int Port { get; set; } = -1;
        public Dictionary<DateTime, string> Messages { get; set; } = new Dictionary<DateTime, string>();
        public bool Active { get; set; } = false;
    }
}
