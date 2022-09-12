using RouletteWebApi.DataObjects.RequestObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.UnitTests.Helpers
{
    public static class TestData
    {
        
        public static PlaceBetRequestDTO placeBetRequest => new PlaceBetRequestDTO()
        {
            Bet = ReturnRandom(),
            Stake = ReturnRandomStake()
        };

        public static PlaceBetRequestDTO InvalidBetRequest => new PlaceBetRequestDTO()
        {
            Bet = "unknown",
            Stake = ReturnRandomStake()
        };


        public static PayoutRequestDTO placeBetobj = new PayoutRequestDTO()
        {
            BetReference = "Bet_563303973"
        };

        public static string ReturnRandom()
        {
            var random = new Random();
            return random.Next(0, 36).ToString();
        }

        public static decimal ReturnRandomStake()
        {
            var random = new Random();
            return random.Next(10000);
        }


    }
}
