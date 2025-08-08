
using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.Party
{
    [ExcludeFromCodeCoverage]
    public class CompanyPartyValidator : PartyValidatorBase<Company>
    {
        public CompanyPartyValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetCantBeBlankTextValueMsg("Name"));

            RuleFor(x => x.OverseasTerritory)
                .NotEmpty()
                .When(x => x.OverseasTerritory != null)
                .WithMessage(ValidatorProvider.GetCantBeBlankTextValueMsg("Overseas Territory"));

            RuleFor(x => x.OverseasNumberInTheUnitedKingdom)
                .NotEmpty()
                .When(x => x.OverseasNumberInTheUnitedKingdom != null)
                .WithMessage(ValidatorProvider.GetCantBeBlankTextValueMsg("Overseas Number In The UK"));
        }
    }
}
