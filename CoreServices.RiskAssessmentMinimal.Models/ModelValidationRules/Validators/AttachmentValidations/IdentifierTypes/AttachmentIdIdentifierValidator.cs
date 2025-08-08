using Edrs.ActionListenerService.Models.DTO.AttachmentDtos.Types.IdentifierTypes;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.AttachmentValidations.IdentifierTypes
{
    [ExcludeFromCodeCoverage]
    public class AttachmentIdIdentifierValidator : IdentifierValidatorBase<AttachmentIdIdentifier>
    {
        public AttachmentIdIdentifierValidator()
        {
            RuleFor(x => x.Value)
                .GreaterThan(0)
                .WithMessage(ValidatorProvider.GetMustBeGreaterThanOneMsg("Attachment Id {PropertyName} - '{PropertyValue}'"));
        }
    }
}
