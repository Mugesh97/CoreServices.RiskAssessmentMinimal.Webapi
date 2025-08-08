using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;


namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.Party
{
    [ExcludeFromCodeCoverage]
    public class PersonPartyValidator : PartyValidatorBase<Person>
    {
        public PersonPartyValidator()
        {
            RuleFor(x => x.Forename)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetCantBeBlankTextValueMsg("Forename"));

            RuleForEach(x => x.Surname)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetCantBeBlankTextValueMsg("Surname"));
        }
    }
}
