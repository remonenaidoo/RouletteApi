using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.DataObjects.DataObjects
{
   public class UserDTO
    {
        public bool UserExists { get; set; }
        public string EmailAddress { get; set; }
        public decimal balance { get; set; }
        public int ClientID { get; set; }


    }
}
