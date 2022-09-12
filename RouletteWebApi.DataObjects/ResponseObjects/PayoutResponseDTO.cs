using RouletteWebApi.DataObjects.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.DataObjects.ResponseObjects
{
    public class PayoutResponseDTO
    {
        public bool IsSuccessful { get; set; }
        public bool WonBet { get; set; }
        public decimal PaidOutAmount { get; set; }
        public InitialBetDTO Bet { get; set; }
        public SpinResponseDTO Spin { get; set; }

    }
}
