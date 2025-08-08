using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;


namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators
{
    [ExcludeFromCodeCoverage]
    public class OtherConveyancerValidator : AbstractValidator<OtherConveyancer>
    {
        public OtherConveyancerValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetCantBeBlankTextValueMsg("Name"));

            RuleFor(x => x.Reference)
                .NotEmpty()
                .WithMessage(other => $"Other Conveyancer '{other.Name}' Reference must be set.");

            RuleFor(r => r.Representees)
                .Must(x => x != null && x.Length > 0)
                .WithMessage(other => $"Other Conveyancer '{other.Name}' must have some representees.");

            RuleForEach(x => x.Representees)
                .NotNull()
                .SetValidator(ValidatorProvider.GetExactTypeValidator<ApplicationParty>());

            RuleFor(x => x.Address)
                .NotNull()
                .SetValidator(ValidatorProvider.GetPolymorphicValidators<MailAddress>());
        }
    }
}
