using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.TitleNumber
{
    [ExcludeFromCodeCoverage]
    public class NewLeaseTitleNumbersValidator : TitleNumbersValidatorBase<NewLeaseTitleNumbers>
    {
        public NewLeaseTitleNumbersValidator()
        {
            RuleForEach(x => x.AdditionalTitles)
                .NotNull()
                .WithMessage(ValidatorProvider.GetMustBeSetMsg("Additional Title"));

            RuleForEach(x => x.AdditionalTitles)
                .Matches(TitleNumbers.TitleNumberCharacterRestrictionRegex)
                .When(x => (x.AdditionalTitles?.Length ?? 0) > 0)
                .WithMessage("{PropertyName} '{PropertyValue}' - " + TitleNumberFormatErrorMsg);
        }
    }
}
