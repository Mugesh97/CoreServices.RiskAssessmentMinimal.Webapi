using Edrs.ActionListenerService.Models.DTO.AttachmentDtos.Types;
using FluentValidation;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.AttachmentValidations
{
    [ExcludeFromCodeCoverage]
    public class IdentifierValidator : AbstractValidator<Identifier>
    {
        public IdentifierValidator()
        {
            RuleFor(x => x.IdentifierType)
                .Must(value => Enum.IsDefined(typeof(Identifier.Type), value))
                .WithMessage(ValidatorProvider.GetAppearsToHaveInvalidValueMsg(nameof(Identifier.IdentifierType)));
        }
    }
}
