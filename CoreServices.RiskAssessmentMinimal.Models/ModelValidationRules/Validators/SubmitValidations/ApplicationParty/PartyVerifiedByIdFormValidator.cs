using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.ApplicationParty
{
    [ExcludeFromCodeCoverage]
    public class PartyVerifiedByIdFormValidator : ApplicationPartyValidatorBase<PartyVerifiedByIdForm>
    {
        public PartyVerifiedByIdFormValidator()
        {
            RuleFor(x => x.IdentityForm)
                .NotNull()
                .WithMessage(ValidatorProvider.GetMustBeSetMsg("Identity Form"))
                .SetValidator(ValidatorProvider.GetExactTypeValidator<IdentityForm>());
        }
    }
}
