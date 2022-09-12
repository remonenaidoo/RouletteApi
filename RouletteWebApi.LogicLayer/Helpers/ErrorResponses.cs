using RouletteWebApi.DataObjects.Enums;
using RouletteWebApi.LogicLayer.Exceptions;
using RouletteWebApi.LogicLayer.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.LogicLayer.Helpers
{
    public class ErrorResponses: IErrorResponses
    {
        private static readonly Dictionary<ErrorTypes, CustomExceptions> ExceptionMessages = new Dictionary<ErrorTypes, CustomExceptions>
        {
            {ErrorTypes.InvalidUser, new CustomExceptions("User does not exist in database") },
            {ErrorTypes.LowBalance, new CustomExceptions("Insufficient balance") },
            {ErrorTypes.InvalidBetReference, new CustomExceptions("Cannot find original bet invalid bet reference") }
       };

        public async Task<CustomExceptions> GetErrorReponse(ErrorTypes errorType)
        {
            var mappedResponse = ExceptionMessages.FirstOrDefault(x => x.Key == errorType).Value;

            if (mappedResponse != null)
                return mappedResponse;
            else
                throw new CustomExceptions("Unable to map error responses");
        }


    }
}
