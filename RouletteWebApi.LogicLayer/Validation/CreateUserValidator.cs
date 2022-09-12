using FluentValidation;
using RouletteWebApi.DataObjects.RequestObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.LogicLayer.Validation
{
    public class CreateUserValidator: AbstractValidator<CreateUserRequestDTO>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.EmailAddress).EmailAddress()
                .NotEmpty().WithMessage("Email is required to create user");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required to create user");

            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required to create user");
        }
    }
}
