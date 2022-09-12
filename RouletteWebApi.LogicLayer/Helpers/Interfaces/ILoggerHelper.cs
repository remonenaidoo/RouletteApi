using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.LogicLayer.Helpers.Interfaces
{
    public interface ILoggerHelper
    {
        void LogInfo(string Message);
        void LogError(string Message);

    }
}
