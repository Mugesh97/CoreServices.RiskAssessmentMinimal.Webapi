using Edrs.ActionListenerService.Models.DTO.AttachmentDtos.Types;
using FluentValidation;
using System;
using System.Diagnostics.CodeAnalysis;
using static Edrs.ActionListenerService.Models.Common.Enums;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.AttachmentValidations
{
    [ExcludeFromCodeCoverage]
    public class AttachmentValidator : ItemValidatorBase<Attachment>
    {
        public AttachmentValidator()
        {
            RuleFor(x => x.CopyType)
                .Must(value => Enum.IsDefined(typeof(DocumentCopyType), value))
                .WithMessage(ValidatorProvider.GetAppearsToHaveInvalidValueMsg(nameof(Attachment.CopyType)));

            RuleFor(x => x.Identifier)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetMustBeSetMsg(nameof(Attachment.Identifier)));

            RuleFor(x => x.ItemType)
                .Must(value => Enum.IsDefined(typeof(Item.Type), value))
                .WithMessage(ValidatorProvider.GetAppearsToHaveInvalidValueMsg(nameof(Attachment.ItemType)));

            RuleFor(x => x.Content)
                .NotNull()
                .WithMessage(ValidatorProvider.GetMustBeSetMsg(nameof(Attachment.Content)))
                .SetValidator(ValidatorProvider.GetExactTypeValidator<AttachmentContent>());
            RuleFor(x => x.Content.Name)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetMustBeSetMsg(nameof(Attachment.Content.Name)));
        }
    }
}
