using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.TitleNumber
{
    [ExcludeFromCodeCoverage]
    public class LeaseExtensionTitleNumbersValidator : TitleNumbersValidatorBase<LeaseExtensionTitleNumbers>
    {
        public LeaseExtensionTitleNumbersValidator()
        {
            RuleFor(x => x.LesseeTitle)
                .NotNull()
                .WithMessage(ValidatorProvider.GetMustBeSetMsg("Lessee Title"));

            RuleFor(x => x.LesseeTitle)
                .Matches(TitleNumbers.TitleNumberCharacterRestrictionRegex)
                .When(x => !string.IsNullOrEmpty(x.LesseeTitle))
                .WithMessage("{PropertyName} '{PropertyValue}' - " + TitleNumberFormatErrorMsg);

            When(x => x.AdditionalTitles?.Any() ?? false, () =>
            {
                RuleForEach(x => x.AdditionalTitles)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotNull()
                    .WithMessage(ValidatorProvider.GetMustBeSetMsg("Additional Title"))
                    .Matches(TitleNumbers.TitleNumberCharacterRestrictionRegex)
                    .WithMessage("{PropertyName} '{PropertyValue}' - " + TitleNumberFormatErrorMsg);
            });
        }
    }
}
