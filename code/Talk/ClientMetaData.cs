using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talk
{
    public class ClientMetaData
    {
        public string Endpoint { get; set; } = "not set";
        public List<Transaction> Transactions { get; }

        public ClientMetaData()
        {
            Transactions = [];   
        }
    }
}
