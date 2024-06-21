using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheService.Protocol
{
    public class Enums
    {
        public enum Commands
        {
            Get,
            Set,
            List,
            Info,
            Delete,
            Clients
        }
    }
}
