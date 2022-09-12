using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.DataObjects.RequestObjects
{
    public class PlaceBetRequestDTO
    {
        public decimal Stake { get; set; }
        public string Bet { get; set; }
    }
}
