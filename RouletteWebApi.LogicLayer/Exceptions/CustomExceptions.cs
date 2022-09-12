using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.LogicLayer.Exceptions
{
    public class CustomExceptions : Exception 
    {
        public CustomExceptions() : base() { }

        public CustomExceptions(string message) : base(message) { }

        public CustomExceptions(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

    }
}
