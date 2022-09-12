using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.LogicLayer.Helpers.Interfaces
{
    public interface ITokenHelper
    {
        Task<string> GenerateJwtToken(string userName);
        Task<ClaimsPrincipal> GetValueFromToken(string token, string jwtSecretToken);
        Task<T> GetClaimValue<T>(string type, ClaimsIdentity identity);
    }
}
