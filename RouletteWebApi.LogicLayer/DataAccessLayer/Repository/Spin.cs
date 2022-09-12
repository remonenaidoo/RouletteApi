using Dapper;
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
    public class Spin : ISpin
    {
        private readonly IDbContext _context;

        public Spin(IDbContext context)
        {
            _context = context;
        }

        public async Task CreateSpin(SpinResponseDTO Spins)
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(SqlQueries.InsertSpins, new { Spins.BetID, Spins.Number, Spins.Colour, Spins.Parity, Spins.BetRange }, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<SpinResponseDTO>> ShowPreviousSpins()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<SpinResponseDTO>(SqlQueries.RetrieveSpins, commandType: CommandType.StoredProcedure);
        }
    }
}
