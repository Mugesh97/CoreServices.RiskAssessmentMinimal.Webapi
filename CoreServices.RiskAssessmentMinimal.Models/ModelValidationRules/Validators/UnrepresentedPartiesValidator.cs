using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators
{
    [ExcludeFromCodeCoverage]
    public class UnrepresentedPartiesValidator : AbstractValidator<UnrepresentedParties>
    {
        public UnrepresentedPartiesValidator()
        {
            RuleForEach(x => x.PartiesVerifiedByIdForms)
                .NotNull()
                .SetValidator(ValidatorProvider.GetExactTypeValidator<PartyVerifiedByIdForm>());

            RuleForEach(x => x.PartiesVerifiedWithoutIdForms)
                .NotNull()
                .SetValidator(ValidatorProvider.GetExactTypeValidator<ApplicationParty>());
        }
    }
}
