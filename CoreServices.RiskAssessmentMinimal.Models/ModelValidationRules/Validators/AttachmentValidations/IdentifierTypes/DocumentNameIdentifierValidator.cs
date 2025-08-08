using Edrs.ActionListenerService.Models.DTO.AttachmentDtos.Types.IdentifierTypes;
using FluentValidation;
using System;
using System.Diagnostics.CodeAnalysis;
using static Edrs.ActionListenerService.Models.Common.Enums;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.AttachmentValidations.IdentifierTypes
{
    [ExcludeFromCodeCoverage]
    public class DocumentNameIdentifierValidator : IdentifierValidatorBase<DocumentNameIdentifier>
    {
        public DocumentNameIdentifierValidator()
        {
            RuleFor(x => x.Value)
                .Must(value => Enum.IsDefined(typeof(DocumentNameType), value))
                .WithMessage(ValidatorProvider.GetAppearsToHaveInvalidValueMsg("Document Name") + " Value is :'{PropertyValue}'");
        }
    }
}
