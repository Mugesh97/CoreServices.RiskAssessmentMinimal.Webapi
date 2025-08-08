using Edrs.ActionListenerService.Models.Common;
using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using FluentValidation;
using System;
using System.Diagnostics.CodeAnalysis;
namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.ApplicationDoc
{
    [ExcludeFromCodeCoverage]
    public class SupportingDocumentValidator : DocumentValidatorBase<SupportingDocument>
    {
        public SupportingDocumentValidator()
        {
            RuleFor(x => x.DocumentName)
                .Must(value => Enum.IsDefined(typeof(Enums.DocumentNameType), value))
                .WithMessage(ValidatorProvider.GetAppearsToHaveInvalidValueMsg("Document Name") + " Value is :\"{PropertyValue}\"");

            RuleFor(x => x.AttachmentId)
                .GreaterThan(0)
                .WithMessage(ValidatorProvider.GetMustBeGreaterThanOneMsg(nameof(Document.AttachmentId)));
        }
    }
}
