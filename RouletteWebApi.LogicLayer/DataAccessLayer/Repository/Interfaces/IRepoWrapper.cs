using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.LogicLayer.DataAccessLayer.Repository.Interfaces
{
    public interface IRepoWrapper
    {
        IPayout Payout { get; }
        IPlaceBet PlaceBet { get; }
        ISpin Spin { get; }
        Iuser User { get; }
    }
}
