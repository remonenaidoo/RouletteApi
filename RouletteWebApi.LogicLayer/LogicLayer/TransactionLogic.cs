using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RouletteWebApi.DataObjects.Constants;
using RouletteWebApi.DataObjects.DataObjects;
using RouletteWebApi.DataObjects.Enums;
using RouletteWebApi.DataObjects.RequestObjects;
using RouletteWebApi.DataObjects.ResponseObjects;
using RouletteWebApi.LogicLayer.DataAccessLayer.Interfaces;
using RouletteWebApi.LogicLayer.DataAccessLayer.Repository.Interfaces;
using RouletteWebApi.LogicLayer.Exceptions;
using RouletteWebApi.LogicLayer.Helpers.Interfaces;
using RouletteWebApi.LogicLayer.LogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteWebApi.LogicLayer.LogicLayer
{
    public class TransactionLogic : ITransactionLogic
    {
        private readonly ILoggerHelper _loggerHelper;
        private readonly IOptions<AppSettings> _appConfig;
        private readonly IBetHelper _betHelper;
        private readonly Random _random;
        private readonly IRepoWrapper _repoWrapper;
        private readonly BetOptions _betOptions;
        private readonly IErrorResponses _errorResponses;

        public TransactionLogic( ILoggerHelper loggerHelper, 
            IOptions<AppSettings> appConfig, IBetHelper betHelper, IRepoWrapper repoWrapper, IErrorResponses errorResponses) 
        {
            _loggerHelper = loggerHelper;
            _appConfig = appConfig;
            _betHelper = betHelper;
            _random = new Random();
            _repoWrapper = repoWrapper;
            _betOptions = new BetOptions();
            _errorResponses = errorResponses;
        }

        public async Task<BetStrikeResponseDTO> PlaceBet(IHeaderDictionary Header,PlaceBetRequestDTO PlaceBetDataRequest)
        {
            string user = await _betHelper.ReturnUserFromHeaderToken(Header);

            if (user == null)
            {
                throw await _errorResponses.GetErrorReponse(ErrorTypes.InvalidUser);
            }

            UserDTO userDetails = await _repoWrapper.User.ReturnValidatedUserDetails(user);

            if(userDetails.balance <= 0 || PlaceBetDataRequest.Stake > userDetails.balance)
            {
                throw await _errorResponses.GetErrorReponse(ErrorTypes.LowBalance);
            }

                var mappedData = new BetDTO()
                {
                    ClientID = userDetails.ClientID,
                    BetReference = "Bet_" + _random.Next().ToString(),
                    TransactionType = (int)TransactionTypes.StrikeBet,
                    Bet = PlaceBetDataRequest.Bet,
                    Balance = userDetails.balance,
                    Amount = PlaceBetDataRequest.Stake
                };

            var StruckBetResponse = await _repoWrapper.PlaceBet.StrikeBet(mappedData);
            
            await _repoWrapper.User.UpdateClientBalance(mappedData);

            return StruckBetResponse;
        }

        public async Task<SpinResponseDTO> SpinRoulette(int BetID)
        {
                var RouletteNumber = _random.Next(_betOptions.MinBet, _betOptions.MaxBet);

                var Spin = new SpinResponseDTO
                {
                    BetID = BetID,
                    IsSuccess = true,
                    Colour = await _betHelper.ReturnBetColour(RouletteNumber),
                    Number = RouletteNumber,
                    Parity = await _betHelper.ReturnBetParity(RouletteNumber),
                    BetRange = await _betHelper.ReturnBetRange(RouletteNumber)
                };

                await _repoWrapper.Spin.CreateSpin(Spin);

                return Spin;
        }

        public async Task<IEnumerable<SpinResponseDTO>> ShowPreviousSpins(IHeaderDictionary Header)
        {
            return await _repoWrapper.Spin.ShowPreviousSpins();
        }

        public async Task<PayoutResponseDTO> Payout(IHeaderDictionary Header, PayoutRequestDTO PayoutRequest)
        {
            var OriginalBet = await _repoWrapper.PlaceBet.GetOriginalBet(PayoutRequest.BetReference);

            if(OriginalBet == null)
            {
                throw await _errorResponses.GetErrorReponse(ErrorTypes.InvalidBetReference);
            }

            var Spins = await SpinRoulette(OriginalBet.BetID);
            var BetWinInfo = await _betHelper.ReturnBetResult(OriginalBet, Spins);

            var mappedData = new BetDTO()
            {
                ClientID = OriginalBet.ClientID,
                BetReference = OriginalBet.BetReference,
                TransactionType = (int)TransactionTypes.ResultBet,
                Amount = BetWinInfo.IsSuccess ? OriginalBet.Amount + (OriginalBet.Amount * BetWinInfo.PayoutRate) : 0,
                Balance = OriginalBet.Balance,
                PayoutInfo = BetWinInfo.IsSuccess ? $"won {BetWinInfo.PayoutRate} * {OriginalBet.Amount}": $" Losing bet: original bet: {OriginalBet.Bet} Results: {JsonConvert.SerializeObject(Spins)}"
              
            };

            await _repoWrapper.Payout.CreateTransaction(mappedData);

            await _repoWrapper.User.UpdateClientBalance(mappedData);


            PayoutResponseDTO payoutResponse = new()
            {
                IsSuccessful = true,
                WonBet = BetWinInfo.IsSuccess,
                PaidOutAmount = mappedData.Amount,
                Bet = OriginalBet,
                Spin = Spins
            };
            return payoutResponse;

        }
    }
}
