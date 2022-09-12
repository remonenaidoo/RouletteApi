using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.DataObjects.ResponseObjects
{
    public class SpinResponseDTO
    {
        public int BetID { get; set; }
        public bool IsSuccess { get; set; }
        public int Number { get; set; }
        public string Colour { get; set; }
        public string Parity { get; set; }
        public string BetRange { get; set; }
    }
}
