
using FluentValidation;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.Role
{
    [ExcludeFromCodeCoverage]
    public class RoleValidator : RoleValidatorBase<DTO.SubmitDtos.Types.Role>
    {
        public RoleValidator()
        {
            RuleFor(x => x.ApplicationId)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetMustBeSetMsg("ApplicationId"));

            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetMustBeSetMsg("Type"));

            RuleFor(x => x.Type)
                .Must(value => Enum.IsDefined(typeof(DTO.SubmitDtos.Types.Role.RoleType),value))
                .WithMessage(ValidatorProvider.GetAppearsToHaveInvalidValueMsg("Type"));
        }
    }
}
