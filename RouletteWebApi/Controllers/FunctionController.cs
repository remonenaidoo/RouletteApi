using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RouletteWebApi.DataObjects.Enums;
using RouletteWebApi.DataObjects.RequestObjects;
using RouletteWebApi.DataObjects.ResponseObjects;
using RouletteWebApi.Filters;
using RouletteWebApi.LogicLayer.LogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteWebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class FunctionController : ControllerBase
    {
        private readonly ITransactionLogic _transactionLogic;

        public FunctionController(ITransactionLogic transactionLogic)
        {
            _transactionLogic = transactionLogic;
        }

        [HttpPost]
        [Route("PlaceBet")]
        public async Task<BetStrikeResponseDTO> PlaceBet([FromBody] PlaceBetRequestDTO PlaceBetRequest)
        {
            return await _transactionLogic.PlaceBet(Request.Headers,PlaceBetRequest);
        }

        [HttpPost]
        [TypeFilter(typeof(DuplicatePayoutCheckFilter))]
        [Route("Payout")]
        public async Task<PayoutResponseDTO> Payout([FromBody] PayoutRequestDTO payoutRequest)
        {
            return await _transactionLogic.Payout(Request.Headers, payoutRequest);
        }

        [HttpPost]
        [Route("PlayRound")]
        public async Task<PayoutResponseDTO> PlayRound([FromBody] PlaceBetRequestDTO PlaceBetRequest)
        {
            var BetPlaced =  await _transactionLogic.PlaceBet(Request.Headers, PlaceBetRequest);

            var PayoutRequest = new PayoutRequestDTO()
            {
                BetReference = BetPlaced.BetReference
            };

            return await _transactionLogic.Payout(Request.Headers, PayoutRequest);
        }

        //[HttpGet]
        //[Route("Spin")]
        //public async Task<SpinResponseDTO> Spin()
        //{
        //    return await _transactionLogic.Spin(Request.Headers);
        //}

        //[HttpGet]
        //[Route("ShowPreviousSpins")]
        //public async Task<IEnumerable<SpinResponseDTO>> ShowPreviousSpins()
        //{
        //    return await _transactionLogic.ShowPreviousSpins(Request.Headers);
        //}

    }
}
