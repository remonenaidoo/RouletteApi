using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.DataObjects.Constants
{
    public class BetOptions
    {
        public int[] NumbersRed = { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };

        public int[] NumbersBlack = { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };

        public int NumbersGreen = 0;

        public int MinBet = 0;

        public int MidBet = 18;

        public int MaxBet = 36;

        public string[] Types = { "red", "black", "even", "odd", "low", "high" };
    }
}
