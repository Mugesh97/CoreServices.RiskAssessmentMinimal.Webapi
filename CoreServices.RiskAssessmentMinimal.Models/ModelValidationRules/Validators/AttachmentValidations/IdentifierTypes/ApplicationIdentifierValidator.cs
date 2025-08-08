using Edrs.ActionListenerService.Models.DTO.AttachmentDtos.Types.IdentifierTypes;
using FluentValidation;
using System;
using System.Diagnostics.CodeAnalysis;
using static Edrs.ActionListenerService.Models.Common.Enums;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.AttachmentValidations.IdentifierTypes
{
    [ExcludeFromCodeCoverage]
    public class ApplicationIdentifierValidator : IdentifierValidatorBase<ApplicationIdentifier>
    {
        public ApplicationIdentifierValidator()
        {
            RuleFor(x => x.Value)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetAppearsToHaveInvalidValueMsg(nameof(ApplicationIdentifier.Value)));

            RuleFor(x => x.Value)
                .Must(value => Enum.IsDefined(typeof(AppType), value))
                .WithMessage(ValidatorProvider.GetAppearsToHaveInvalidValueMsg("Application Type") + " Value is :'{PropertyValue}'");
        }
    }
}
