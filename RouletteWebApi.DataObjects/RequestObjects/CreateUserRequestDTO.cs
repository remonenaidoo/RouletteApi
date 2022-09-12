using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.DataObjects.RequestObjects
{
   public class CreateUserRequestDTO
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public decimal Balance { get; set; }
        public string Password { get; set; }

    }
}
