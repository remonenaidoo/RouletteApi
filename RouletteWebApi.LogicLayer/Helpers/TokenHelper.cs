using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RouletteWebApi.DataObjects.DataObjects;
using RouletteWebApi.LogicLayer.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.LogicLayer.Helpers
{
    public class TokenHelper: ITokenHelper
    {

        private readonly IConfiguration _config;

        public TokenHelper(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<string> GenerateJwtToken(string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, userName)}),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<ClaimsPrincipal> GetValueFromToken(string token, string jwtSecretToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretToken))
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken securityToken);

                return principal;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<T> GetClaimValue<T>(string type, ClaimsIdentity identity)
        {
            var claims = identity?.Claims.ToList();
            var value = claims?.SingleOrDefault()?.Value;

            var tType = typeof(T);

            if (tType == typeof(string))
                return (T)(object)value;

            if (tType == typeof(int))
                return (T)(object)Convert.ToInt32(value);

            if (tType == typeof(long))
                return (T)(object)Convert.ToInt64(value);

            if (tType == typeof(DateTime))
                return (T)(object)Convert.ToDateTime(value);

            if (tType == typeof(bool))
                return (T)(object)Convert.ToBoolean(value);

            if (tType == typeof(decimal))
                return (T)(object)Convert.ToDecimal(value);

            //return string by default
            return (T)(object)value;
        }
    }
}
