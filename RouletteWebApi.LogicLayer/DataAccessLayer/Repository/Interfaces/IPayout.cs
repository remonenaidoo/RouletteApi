using RouletteWebApi.DataObjects.DataObjects;
using RouletteWebApi.DataObjects.ResponseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.LogicLayer.DataAccessLayer.Repository.Interfaces
{
    public interface IPayout
    {
        Task CreateTransaction(BetDTO transactions);
        Task<BetStrikeResponseDTO> RetrievePayoutInfo(string BetReference);
    }
}
