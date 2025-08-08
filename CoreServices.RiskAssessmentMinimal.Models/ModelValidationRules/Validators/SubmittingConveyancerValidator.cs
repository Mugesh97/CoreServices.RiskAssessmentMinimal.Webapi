using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators
{
    [ExcludeFromCodeCoverage]
    public class SubmittingConveyancerValidator : AbstractValidator<SubmittingConveyancer>
    {
        public SubmittingConveyancerValidator()
        {
            RuleFor(x => x.CaseReference)
                .Matches(SubmittingConveyancer.CaseReferenceRestrictionRegex)
                .WithMessage(ValidatorProvider.GetAppearsToHaveInvalidValueMsg("Case Reference"));

            //RuleFor(x => x.Name)
            //    .NotEmpty()
            //    .WithMessage(ValidatorProvider.GetCantBeBlankTextValueMsg("Name"));

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetCantBeBlankTextValueMsg("Email"));

            RuleFor(x => x.PhoneNo)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetCantBeBlankTextValueMsg("PhoneNo"));

            RuleFor(x => x.Representees)
                .Must(x => x != null && x.Length > 0)
                .WithMessage(submitting => $"Submitting Conveyancer '{submitting.CaseReference}' must have some representees.");

            RuleFor(x => x.Representees)
                .Must(x => x.Count(r => r.IsApplicant) > 0)
                .WithMessage("At least one of the representees has to be the applicant."); // TODO: Move this to a generic validation of all representees together

            RuleForEach(x => x.Representees)
                .NotNull()
                .SetValidator(ValidatorProvider.GetExactTypeValidator<ApplicationParty>());
        }
    }
}
