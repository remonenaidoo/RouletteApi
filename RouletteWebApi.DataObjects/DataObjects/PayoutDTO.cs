using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.DataObjects.DataObjects
{
    public class PayoutDTO
    {
        public bool IsSuccess { get; set; }
        public int PayoutRate { get; set; }
    }
}
