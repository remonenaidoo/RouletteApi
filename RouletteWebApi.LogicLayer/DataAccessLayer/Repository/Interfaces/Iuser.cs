using RouletteWebApi.DataObjects.DataObjects;
using RouletteWebApi.DataObjects.RequestObjects;
using RouletteWebApi.DataObjects.ResponseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.LogicLayer.DataAccessLayer.Repository.Interfaces
{
    public interface Iuser
    {
        Task<CreateUserResponseDTO> CreateUser(CreateUserRequestDTO UserInfo);
        Task<LoginUserResponseDTO> RetrieveUser(LoginUserRequestDTO UserInfo);
        Task<UserDTO> ReturnValidatedUserDetails(string EmailAddress);
        Task UpdateClientBalance(BetDTO ClientBalance);

    }
}
