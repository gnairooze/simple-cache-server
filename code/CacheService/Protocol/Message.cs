using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheService.Protocol
{
    public class Message
    {
        public string Command { get; set; } = "not set";
        //arguments has name and value
        public List<(string, string)> Arguments { get; }

        public Message()
        {
            Arguments = [];
        }
    }
}
