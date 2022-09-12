using Dapper;
using RouletteWebApi.DataObjects.DataObjects;
using RouletteWebApi.DataObjects.ResponseObjects;
using RouletteWebApi.LogicLayer.DataAccessLayer.Database;
using RouletteWebApi.LogicLayer.DataAccessLayer.Interfaces;
using RouletteWebApi.LogicLayer.DataAccessLayer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.LogicLayer.DataAccessLayer.Repository
{
    public class PlaceBet : IPlaceBet
    {
        private readonly IDbContext _context;

        public PlaceBet(IDbContext context)
        {
            _context = context;
        }

        public async Task<InitialBetDTO> GetOriginalBet(string BetReference)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<InitialBetDTO>(SqlQueries.GetOriginalBet, new { BetReference }, commandType: CommandType.StoredProcedure);
        }

        public async Task<BetStrikeResponseDTO> StrikeBet(BetDTO Bet)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<BetStrikeResponseDTO>(SqlQueries.InsertBets, new {Bet.ClientID,Bet.BetReference,Bet.TransactionType,Bet.Amount,Bet.Bet}, commandType: CommandType.StoredProcedure);
        }

       
    }
}
