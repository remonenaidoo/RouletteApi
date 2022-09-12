using FluentValidation;
using RouletteWebApi.DataObjects.RequestObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.LogicLayer.Validation
{
    public class AuthenticateUserValidator : AbstractValidator<LoginUserRequestDTO>
    {
        public AuthenticateUserValidator()
        {
            RuleFor(x => x.EmailAddress).EmailAddress()
                .NotEmpty().WithMessage("Email is required to authenticate user");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required to authenticate user");
        }
    }
}
