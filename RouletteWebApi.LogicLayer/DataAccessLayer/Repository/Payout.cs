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
    public class Payout : IPayout
    {
        private readonly IDbContext _context;

        public Payout(IDbContext context)
        {
            _context = context;
        }

        public async Task CreateTransaction(BetDTO transactions)
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(SqlQueries.InsertPayout, new { transactions.BetReference, transactions.TransactionType, transactions.Amount, transactions.PayoutInfo }, commandType: CommandType.StoredProcedure);
        }

        public async Task<BetStrikeResponseDTO> RetrievePayoutInfo(string BetReference)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<BetStrikeResponseDTO>(SqlQueries.RetrivePayout, new {BetReference}, commandType: CommandType.StoredProcedure);
        }
    }
}
