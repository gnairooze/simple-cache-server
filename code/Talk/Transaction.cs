using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talk
{
    public class Transaction
    {
        public DateTime RunDate { get; set; } = DateTime.Now;
        public string Message { get; set; } = "not set";
        public string Response { get; set; } = "not set";
    }
}
