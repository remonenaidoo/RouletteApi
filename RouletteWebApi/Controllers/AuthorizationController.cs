using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUserLogic _userLogic;

        public AuthorizationController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<CreateUserResponseDTO> CreateUser([FromBody] CreateUserRequestDTO UserInfo)
        {
            return await _userLogic.CreateUser(UserInfo);
        }

        [HttpPost]
        [Route("AuthenticateUser")]
        public async Task<AuthenticateUserResponseDTO> AuthenticateUser([FromBody] LoginUserRequestDTO UserCreds)
        {
            return await _userLogic.AuthenticateUser(UserCreds);
        }

    }
}
