using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.DataObjects.DataObjects
{
    public class BaseBalanceDTO
    {
        public int ClientID { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
    }
}
