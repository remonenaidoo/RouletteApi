using RouletteWebApi.DataObjects.ResponseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.LogicLayer.DataAccessLayer.Repository.Interfaces
{
    public interface ISpin
    {
        Task CreateSpin(SpinResponseDTO Spins);
        Task<IEnumerable<SpinResponseDTO>> ShowPreviousSpins();
    }
}
