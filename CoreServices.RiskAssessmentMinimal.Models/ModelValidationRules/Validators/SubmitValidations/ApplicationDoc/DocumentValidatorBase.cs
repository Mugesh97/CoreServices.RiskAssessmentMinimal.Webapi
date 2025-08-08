using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.ApplicationDoc
{
    [ExcludeFromCodeCoverage]
    public class DocumentValidatorBase<TDocument> : AbstractValidator<TDocument> where TDocument : Document
    {
        public DocumentValidatorBase()
        {
        }
    }
}
