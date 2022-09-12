using RouletteWebApi.DataObjects.DataObjects;
using RouletteWebApi.DataObjects.ResponseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.LogicLayer.DataAccessLayer.Repository.Interfaces
{
    public interface IPlaceBet
    {
        Task<InitialBetDTO> GetOriginalBet(string BetReference);
        Task<BetStrikeResponseDTO> StrikeBet(BetDTO Bet);
       
    }
}
