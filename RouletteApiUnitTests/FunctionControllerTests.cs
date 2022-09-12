using RouletteWebApi.UnitTests.Helpers;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RouletteWebApi.Controllers;
using RouletteWebApi.DataObjects.DataObjects;
using RouletteWebApi.DataObjects.RequestObjects;
using RouletteWebApi.DataObjects.ResponseObjects;
using RouletteWebApi.LogicLayer.DataAccessLayer.Interfaces;
using RouletteWebApi.LogicLayer.DataAccessLayer.Repository.Interfaces;
using RouletteWebApi.LogicLayer.LogicLayer.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using HostHelper = RouletteWebApi.UnitTests.Helpers.HostTestHelper;
using TestData = RouletteWebApi.UnitTests.Helpers.TestData;
using Microsoft.AspNetCore.Http;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace RouletteApiUnitTests
{
    public class FunctionControllerTests
    {
        private readonly ITransactionLogic _itransactionLogic;
        private readonly IRepoWrapper _repoWrapper;
        private readonly FunctionController _functionController;
        private readonly IDbContext _context;
        public FunctionControllerTests()
        {
            var _serviceScopeFactory = HostHelper.Host.Services.GetService(typeof(IServiceScopeFactory)) as IServiceScopeFactory;

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                _itransactionLogic = scope.ServiceProvider.GetRequiredService<ITransactionLogic>();
                _repoWrapper = scope.ServiceProvider.GetRequiredService<IRepoWrapper>();
                _context = scope.ServiceProvider.GetRequiredService<IDbContext>();
               
            }
            _functionController = new FunctionController(_itransactionLogic);
        }

        [Fact]
        public async Task CreateBet()
        {
            var BetResponse = (await _functionController.PlaceBet(TestData.placeBetRequest)) as BetStrikeResponseDTO;
            
            Assert.True(BetResponse.IsSuccess);
        }

        [Fact]
        public async Task ConfirmBetSaved()
        {
            var BetResponse = (await _functionController.PlaceBet(TestData.placeBetRequest)) as BetStrikeResponseDTO;

            Assert.True(await ConfirmedSavedBet(BetResponse.BetReference));
        }

        //[Fact]
        //public async Task SpinRoulette()
        //{
        //    var SpinResponse = (await _functionController.Spin()) as SpinResponseDTO;

        //    Assert.True(SpinResponse.IsSuccess);
        //}

        [Fact]
        public async Task PayoutBet()
        {
            var BetResponse = (await _functionController.PlaceBet(TestData.placeBetRequest)) as BetStrikeResponseDTO;

            var PayoutRequest = new PayoutRequestDTO()
            {
                BetReference = BetResponse.BetReference
            };

          var PayoutResponse = (await _functionController.Payout(PayoutRequest)) as PayoutResponseDTO;

          Assert.True(PayoutResponse.IsSuccessful);
        }

        private async Task<bool> ConfirmedSavedBet(string BetReference)
        {
            var OriginalBetData = await _repoWrapper.PlaceBet.GetOriginalBet(BetReference);
            return OriginalBetData != null ? true : false; 
        }
    }
}
