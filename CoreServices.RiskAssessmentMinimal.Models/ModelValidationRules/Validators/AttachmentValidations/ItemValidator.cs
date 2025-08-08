using Edrs.ActionListenerService.Models.DTO.AttachmentDtos.Types;
using FluentValidation;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.AttachmentValidations
{
    [ExcludeFromCodeCoverage]
    public class ItemValidator : AbstractValidator<Item>
    {
        public ItemValidator()
        {
            RuleFor(x => x.ItemType)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetAppearsToHaveInvalidValueMsg(nameof(Attachment.CopyType)));

            RuleFor(x => x.ItemType)
                .Must(value => Enum.IsDefined(typeof(Item.Type), value))
                .WithMessage(ValidatorProvider.GetAppearsToHaveInvalidValueMsg(nameof(Item.ItemType)));
        }
    }
}
