using Edrs.ActionListenerService.Models.DTO.AttachmentDtos.Types;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.AttachmentValidations.IdentifierTypes
{
    [ExcludeFromCodeCoverage]
    public class IdentifierValidatorBase<TIdentifier> : AbstractValidator<TIdentifier> where
        TIdentifier : Identifier
    {
    }
}
