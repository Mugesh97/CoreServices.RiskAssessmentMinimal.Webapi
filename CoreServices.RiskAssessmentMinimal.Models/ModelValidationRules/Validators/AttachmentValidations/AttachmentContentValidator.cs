using Edrs.ActionListenerService.Models.DTO.AttachmentDtos.Types;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.AttachmentValidations
{
    [ExcludeFromCodeCoverage]
    public class AttachmentContentValidator : AbstractValidator<AttachmentContent>
    {
        public AttachmentContentValidator()
        {
            RuleFor(x => x.Data)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetAppearsToHaveInvalidValueMsg(nameof(AttachmentContent.Data)));
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetAppearsToHaveInvalidValueMsg(nameof(AttachmentContent.Name)));

        }
    }
}
