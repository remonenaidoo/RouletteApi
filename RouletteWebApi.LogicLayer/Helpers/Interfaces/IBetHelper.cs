using Microsoft.AspNetCore.Http;
using RouletteWebApi.DataObjects.DataObjects;
using RouletteWebApi.DataObjects.ResponseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.LogicLayer.Helpers.Interfaces
{
    public interface IBetHelper
    {
        Task<string> ReturnBetColour(int RouletteNumber);
        Task<string> ReturnBetParity(int RouletteNumber);
        Task<string> ReturnBetRange(int RouletteNumber);
        Task<PayoutDTO> ReturnBetResult(InitialBetDTO OriginalBet, SpinResponseDTO Spins);
        Task<string> ReturnUserFromHeaderToken(IHeaderDictionary headerDictionary);
    }
}
