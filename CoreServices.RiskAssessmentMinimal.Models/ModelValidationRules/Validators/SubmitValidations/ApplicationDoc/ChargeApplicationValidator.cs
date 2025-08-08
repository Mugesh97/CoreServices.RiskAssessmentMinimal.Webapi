using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using FluentValidation;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.ApplicationDoc
{
    [ExcludeFromCodeCoverage]
    public class ChargeApplicationValidator : ApplicationValidatorBase<ChargeApplication>
    {
        public ChargeApplicationValidator()
        {
            RuleFor(x => x.LendersSortCode)
                .NotEqual(string.Empty)
                .WithMessage(ValidatorProvider.GetCantBeBlankTextValueMsg("Lender's Sort Code"));

            RuleFor(x => x.ChargeDate)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetCantBeBlankTextValueMsg(nameof(ChargeApplication.ChargeDate)));

            RuleFor(x => x.ChargeDate)
                .NotEqual(DateTime.MinValue)
                .WithMessage(ValidatorProvider.GetCantBeBlankTextValueMsg(nameof(ChargeApplication.ChargeDate)));

            //RuleFor(x => x.AttachmentRef)
            //    .NotEmpty()
            //    .WithMessage(ValidatorProvider.GetMustHaveUniqueAttachmentIdMsg(nameof(Document.AttachmentRef)));

            RuleFor(x => x.AttachmentId)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetMustBeGreaterThanOneMsg(nameof(Document.AttachmentId)));
        }
    }
}
