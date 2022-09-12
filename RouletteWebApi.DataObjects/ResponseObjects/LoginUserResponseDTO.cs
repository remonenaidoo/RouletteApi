using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.DataObjects.ResponseObjects
{
    public class LoginUserResponseDTO
    {
        public bool responseMessage { get; set; }
        public string EmailAddress { get; set; }
    }
}
