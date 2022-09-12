using RouletteWebApi.DataObjects.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.DataObjects.ResponseObjects
{
    public class BetStrikeResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string BetReference { get; set; }
    }
}
