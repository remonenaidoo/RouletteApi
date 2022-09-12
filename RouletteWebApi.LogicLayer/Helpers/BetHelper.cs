using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using RouletteWebApi.DataObjects.Constants;
using RouletteWebApi.DataObjects.DataObjects;
using RouletteWebApi.DataObjects.ResponseObjects;
using RouletteWebApi.LogicLayer.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.LogicLayer.Helpers
{
    public class BetHelper : IBetHelper
    {
        private readonly BetOptions _BetOptions; private readonly ITokenHelper _tokenHelper; private readonly IConfiguration _config;

        public BetHelper(ITokenHelper tokenHelper, IConfiguration configuration)
        {
            _BetOptions = new BetOptions();
            _tokenHelper = tokenHelper;
            _config = configuration;
        }

        public async Task<string> ReturnBetColour(int RouletteNumber)
        {
            if (_BetOptions.NumbersBlack.Contains(RouletteNumber))
            {
                return BetColours.Black;
            }
            else
            if (_BetOptions.NumbersRed.Contains(RouletteNumber))
            {
                return BetColours.Red;
            }
            else
            {
                return BetColours.Green;
            }
        }
        public async Task<string> ReturnBetParity(int RouletteNumber)
        {
            if (RouletteNumber % 2 == 0)
            {
                return BetParity.Even;
            }
            else
            {
                return BetParity.Odd;
            }
        }
        public async Task<string> ReturnBetRange(int RouletteNumber)
        {
            if (_BetOptions.MinBet >= RouletteNumber && _BetOptions.MidBet <= RouletteNumber)
            {
                return BetRange.Low;
            }
            else
            {
                return BetRange.High;
            }
        }
        public async Task<PayoutDTO> ReturnBetResult(InitialBetDTO OriginalBet, SpinResponseDTO Spins)
        {
            if (OriginalBet.Bet == Spins.Parity)
                return new PayoutDTO { IsSuccess = true, PayoutRate = PayoutRates.Parity };
            if (OriginalBet.Bet == Spins.Colour)
                return new PayoutDTO { IsSuccess = true, PayoutRate = PayoutRates.ColourRedBlack };
            if (OriginalBet.Bet == Spins.BetRange)
                return new PayoutDTO { IsSuccess = true, PayoutRate = PayoutRates.Range };
            if (OriginalBet.Bet == Spins.Number.ToString())
            {
                return OriginalBet.Bet == 0.ToString() ? new PayoutDTO { IsSuccess = true, PayoutRate = PayoutRates.ColurGreen } : new PayoutDTO { IsSuccess = true, PayoutRate = PayoutRates.Number };
            }
            else
                return new PayoutDTO { IsSuccess = false, PayoutRate = PayoutRates.Default };
        }

        public async Task<string> ReturnUserFromHeaderToken(IHeaderDictionary headerDictionary)
        {
            headerDictionary.TryGetValue("Authorization", out StringValues authorization);

            if (StringValues.IsNullOrEmpty(authorization))
                return "";

            var claimsPrinciple = await _tokenHelper.GetValueFromToken(authorization, _config["Jwt:Key"]);

            if (claimsPrinciple == null)
                return "";

            var simplePrinciple = claimsPrinciple.Identity as ClaimsIdentity;
            var jwtPunterId = simplePrinciple.Name;

            return jwtPunterId;
        }



    }
}
