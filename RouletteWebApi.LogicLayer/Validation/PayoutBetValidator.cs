using FluentValidation;
using RouletteWebApi.DataObjects.RequestObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteWebApi.LogicLayer.Validation
{
    public class PayoutBetValidator : AbstractValidator<PayoutRequestDTO>
    {
        public PayoutBetValidator()
        {
            RuleFor(x => x.BetReference).NotEmpty().WithMessage("BetReference required to process bet");
        }

    }
}
