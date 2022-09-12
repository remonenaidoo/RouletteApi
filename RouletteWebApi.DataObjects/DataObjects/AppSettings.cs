using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.DataObjects.DataObjects
{
    public class AppSettings
    {
        public bool EnableLogger { get; set; }
        public string DBName { get; set; }
        public string PayoutRequestParameter { get; set; }
        public string BetReferenceProperty { get; set; }
        public string PlaceBetRequestParameter { get; set; }
        public string StakeProperty { get; set; }
        public string BetProperty { get; set; }
        public string SqlConnection { get; set; }


    }
}
