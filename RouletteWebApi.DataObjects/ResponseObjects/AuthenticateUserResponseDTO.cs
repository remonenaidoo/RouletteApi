using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.DataObjects.ResponseObjects
{
    public class AuthenticateUserResponseDTO
    {
        public string Token { get; set; }
        public string responseMessage { get; set; }
    }
}
