using Edrs.ActionListenerService.Models.DTO.AttachmentDtos.Types;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.AttachmentValidations
{
    [ExcludeFromCodeCoverage]
    public class ItemValidatorBase<TItem> : AbstractValidator<TItem> where TItem : Item
    {
        public ItemValidatorBase()
        {
        }
    }
}
