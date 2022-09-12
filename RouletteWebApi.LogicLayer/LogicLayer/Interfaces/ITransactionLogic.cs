using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using RouletteWebApi.DataObjects.RequestObjects;
using RouletteWebApi.DataObjects.ResponseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.LogicLayer.LogicLayer.Interfaces
{
    public interface ITransactionLogic
    {
        Task<BetStrikeResponseDTO> PlaceBet(IHeaderDictionary Header, PlaceBetRequestDTO PlaceBetDataRequest);
        Task<SpinResponseDTO> SpinRoulette(int BetID);
        Task<PayoutResponseDTO> Payout(IHeaderDictionary Header,PayoutRequestDTO PayoutRequest);
        Task<IEnumerable<SpinResponseDTO>> ShowPreviousSpins(IHeaderDictionary Header);

    }
}
