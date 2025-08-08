using Edrs.ActionListenerService.Models.DTO.AttachmentDtos.Types;
using Edrs.ActionListenerService.Models.Common;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.AttachmentValidations
{
    [ExcludeFromCodeCoverage]
    public class NoteValidator: ItemValidatorBase<Note>
    {
        public NoteValidator()
        {
            RuleFor(x => x.Notes)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetCantBeBlankTextValueMsg("Notes"))
                .Matches(Constants.NonBlankTextTypeRegex)
                .WithMessage("Notes contains invalid characters.");
        }
    }
}
