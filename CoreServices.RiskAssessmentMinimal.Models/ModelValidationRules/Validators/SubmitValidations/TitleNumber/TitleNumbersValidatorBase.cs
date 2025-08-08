using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using FluentValidation;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.TitleNumber
{
    [ExcludeFromCodeCoverage]
    public class TitleNumbersValidatorBase<TTitle> : AbstractValidator<TTitle> where TTitle : TitleNumbers
    {
        public const string TitleNumberFormatErrorMsg = "Must match a prescribed format.";
        public const string TitleNumberUniquenessErrorMsg = "Title Numbers must be unique.";

        public TitleNumbersValidatorBase()
        {
            RuleFor(x => x.TitleNumbersType)
                .Must(value => Enum.IsDefined(typeof(TitleNumbers.Type), value))
                .WithMessage(ValidatorProvider.GetAppearsToHaveInvalidValueMsg("Title Number Type '{PropertyValue}'"));

            RuleFor(x => x.Titles)
                .Must(x => x != null && x.Length > 0)
                .WithMessage(ValidatorProvider.GetMustBeSetMsg("Titles"));

            //RuleFor(x => x.AllTitlesInTheObject)
            //    .UniqueValues()
            //    .WithMessage(TitleNumberUniquenessErrorMsg);

            When(x => x.Titles?.Any() ?? false, () =>
            {
                RuleForEach(x => x.Titles)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotEmpty()
                    .WithMessage(ValidatorProvider.GetMustBeSetMsg("Title Number"))
                    .Matches(TitleNumbers.TitleNumberCharacterRestrictionRegex)
                    .WithMessage("Title Number '{PropertyValue}' - " + TitleNumberFormatErrorMsg);
            });
        }
    }
}
