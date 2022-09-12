using Microsoft.AspNetCore.Mvc;
using RouletteWebApi.DataObjects.RequestObjects;
using RouletteWebApi.DataObjects.ResponseObjects;
using RouletteWebApi.LogicLayer.DataAccessLayer.Repository.Interfaces;
using RouletteWebApi.LogicLayer.Helpers.Interfaces;
using RouletteWebApi.LogicLayer.LogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.LogicLayer.LogicLayer
{
    public class UserLogic: IUserLogic
    {
        private readonly IRepoWrapper _repoWrapper;
        private readonly ITokenHelper _tokenHelper;

        public UserLogic( IRepoWrapper repoWrapper, ITokenHelper tokenHelper)
        {
            _repoWrapper = repoWrapper;
            _tokenHelper = tokenHelper;
        }

        public async Task<AuthenticateUserResponseDTO> AuthenticateUser(LoginUserRequestDTO UserCreds)
        {
            var AuthenticateUserResponse = new AuthenticateUserResponseDTO();

            var mappedData = new LoginUserRequestDTO()
            {
                EmailAddress = UserCreds.EmailAddress,
                Password = UserCreds.Password
            };
            
            var UserExists =  await _repoWrapper.User.RetrieveUser(mappedData);

            if(UserExists.responseMessage)
            {
                var token = await _tokenHelper.GenerateJwtToken(UserCreds.EmailAddress);
                
                AuthenticateUserResponse.Token = token;
                AuthenticateUserResponse.responseMessage = "Success";

                return AuthenticateUserResponse;
            }

            AuthenticateUserResponse.responseMessage = "Please pass the valid Username and Password";
            return AuthenticateUserResponse;
        }

        public async Task<CreateUserResponseDTO> CreateUser(CreateUserRequestDTO UserInfo)
        {
            var mappedData = new CreateUserRequestDTO()
            {
                Balance = UserInfo.Balance,
                EmailAddress = UserInfo.EmailAddress,
                Password = UserInfo.Password,
                Username = UserInfo.Username,
            };
           return await _repoWrapper.User.CreateUser(mappedData);
        }



    }
}
