using RouletteWebApi.DataObjects.Enums;
using RouletteWebApi.LogicLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.LogicLayer.Helpers.Interfaces
{
    public interface IErrorResponses
    {
        Task<CustomExceptions> GetErrorReponse(ErrorTypes errorType);
    }
}
