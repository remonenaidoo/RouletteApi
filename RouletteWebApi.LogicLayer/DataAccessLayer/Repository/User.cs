using Dapper;
using RouletteWebApi.DataObjects.DataObjects;
using RouletteWebApi.DataObjects.RequestObjects;
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
    public class User : Iuser
    {
        private readonly IDbContext _context;

        public User(IDbContext context)
        {
            _context = context;
        }

        public async Task<CreateUserResponseDTO> CreateUser(CreateUserRequestDTO UserInfo)
        {
           using var connection = _context.CreateConnection();
          return await connection.QueryFirstOrDefaultAsync<CreateUserResponseDTO>(SqlQueries.AddUser, new { UserInfo.Username,UserInfo.EmailAddress, UserInfo.Balance, UserInfo.Password }, commandType: CommandType.StoredProcedure);

        }

        public async Task<LoginUserResponseDTO> RetrieveUser(LoginUserRequestDTO UserInfo)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<LoginUserResponseDTO>(SqlQueries.LoginUser, new { UserInfo.EmailAddress, UserInfo.Password }, commandType: CommandType.StoredProcedure);
        }

        public async Task<UserDTO> ReturnValidatedUserDetails(string EmailAddress)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<UserDTO>(SqlQueries.ValidateUserExists, new { EmailAddress }, commandType: CommandType.StoredProcedure);

        }

        public async Task UpdateClientBalance(BetDTO ClientBalance)
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(SqlQueries.UpdateBalances, new { ClientBalance.ClientID, ClientBalance.BetReference, ClientBalance.TransactionType, ClientBalance.Amount, ClientBalance.Balance }, commandType: CommandType.StoredProcedure);
        }
    }
}
