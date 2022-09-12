using Microsoft.AspNetCore.Mvc;
using RouletteWebApi.DataObjects.RequestObjects;
using RouletteWebApi.DataObjects.ResponseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.LogicLayer.LogicLayer.Interfaces
{
    public interface IUserLogic
    {
        Task<CreateUserResponseDTO> CreateUser(CreateUserRequestDTO UserInfo);

        Task<AuthenticateUserResponseDTO> AuthenticateUser(LoginUserRequestDTO UserCreds);

    }
}
