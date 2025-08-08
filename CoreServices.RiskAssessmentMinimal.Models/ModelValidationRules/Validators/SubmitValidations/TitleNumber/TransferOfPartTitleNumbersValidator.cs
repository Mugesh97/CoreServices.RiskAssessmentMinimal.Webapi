using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.TitleNumber
{
    [ExcludeFromCodeCoverage]
    public class TransferOfPartTitleNumbersValidator : TitleNumbersValidatorBase<TransferOfPartTitleNumbers>
    {
        public TransferOfPartTitleNumbersValidator()
        {
            RuleForEach(x => x.AdditionalTitles)
                .NotNull()
                .WithMessage(ValidatorProvider.GetMustBeSetMsg("Additional Title"))
                .Matches(TitleNumbers.TitleNumberCharacterRestrictionRegex)
                .WithMessage("{PropertyName} '{PropertyValue}' - " + TitleNumberFormatErrorMsg);
        }
    }
}
