using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using FluentValidation;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.ApplicationDoc
{
    [ExcludeFromCodeCoverage]
    public class ApplicationValidatorBase<TApplicationBase> : DocumentValidatorBase<TApplicationBase> where TApplicationBase : ApplicationBase
    {
        public ApplicationValidatorBase()
        {
            RuleFor(x => x.ApplicationId)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetMustBeSetMsg("ApplicationId"));

            RuleFor(x => x.GeneralType)
                .Must(value => Enum.IsDefined(typeof(ApplicationBase.Type), value))
                .WithMessage(a => ValidatorProvider.GetAppearsToHaveInvalidValueMsg($"General Type {a.GeneralType}"));

            RuleFor(x => x.FeeInPence)
                .GreaterThanOrEqualTo(0)
                .WithMessage(ValidatorProvider.GetMustBeGreaterThanOrEqualToZeroMsg("Fee In Pence"));

            RuleFor(x => x.Value)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetMustBeSetMsg("Value"));
        }
    }
}
