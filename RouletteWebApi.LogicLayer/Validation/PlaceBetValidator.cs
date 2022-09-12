using FluentValidation;
using RouletteWebApi.DataObjects.Enums;
using RouletteWebApi.DataObjects.RequestObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RouletteWebApi.DataObjects.Constants;

namespace RouletteWebApi.LogicLayer.Validation
{
    public class PlaceBetValidator : AbstractValidator<PlaceBetRequestDTO>
    {
        public PlaceBetValidator()
        {
            RuleFor(x => x.Stake).GreaterThan(0)
                .WithMessage("Stake needs to be greater than zero")
                .NotEmpty().WithMessage("Stake value is required");

            RuleFor(x => x.Bet).Must((x) => ValidBet(x)).WithMessage("Bet should consist of the following values: 0-36, red, black, even, odd, low, high");
        }

        public BetDataType ReturnDataType(string bet)
        {
            return Int32.TryParse(bet, out _) ? BetDataType.Integer : BetDataType.String;
        }

        public bool ValidBet(string bet)
        {
            BetDataType betDataType = ReturnDataType(bet);
            var BetOptions = new BetOptions();

            if (betDataType == BetDataType.Integer)
            {
                Int32.TryParse(bet, out int BetInfo);

                if (BetInfo >= BetOptions.MinBet && BetInfo <= BetOptions.MaxBet)
                {
                    return true;
                }
                return false;
            }
            else if (betDataType == BetDataType.String)
            {
                if (BetOptions.Types.Contains(bet.ToLower()))
                {
                    return true;
                }
            }
            return false;

        }


    }
}
