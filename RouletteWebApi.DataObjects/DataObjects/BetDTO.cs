using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.DataObjects.DataObjects
{
    public class BetDTO: BaseBalanceDTO
    {
        public string Bet { get; set; }
        public int TransactionType { get; set; }
        public string BetReference { get; set; }
        public string PayoutInfo { get; set; }
    }
}
