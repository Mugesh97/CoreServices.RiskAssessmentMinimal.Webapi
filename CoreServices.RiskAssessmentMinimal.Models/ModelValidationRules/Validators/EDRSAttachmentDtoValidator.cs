using Edrs.ActionListenerService.Models.DTO.AttachmentDtos.Types;
using Edrs.ActionListenerService.Models.Request;
using Edrs.ActionListenerService.Models.Common;
using FluentValidation;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators
{
    [ExcludeFromCodeCoverage]
    public class EDRSAttachmentDtoValidator: AbstractValidator<EDRSAttachmentDto>
    {
        public EDRSAttachmentDtoValidator()
        {
            RuleFor(x => x.ApplicationMessageId)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetCantBeBlankTextValueMsg("Application Message Id"));
            RuleFor(x => x.ExternalReference)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetCantBeBlankTextValueMsg("External Reference"));
            RuleFor(x => x.Item)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetMustBeSetMsg("Item"));

            RuleFor(x => x.Item.ItemType)
                .Must(value => Enum.IsDefined(typeof(Item.Type), value))
                .WithMessage(a => ValidatorProvider.GetAppearsToHaveInvalidValueMsg($"Item {a}"));
            RuleFor(x => x.AdditionalProviderFilter)
                .Matches(Constants.AdditionalProviderFilterRestrictionRegex);

        }
    }
}
