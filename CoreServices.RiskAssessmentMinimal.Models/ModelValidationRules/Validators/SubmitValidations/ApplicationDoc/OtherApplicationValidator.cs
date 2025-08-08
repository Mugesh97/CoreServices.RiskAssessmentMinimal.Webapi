using Edrs.ActionListenerService.Models.Common;
using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using FluentValidation;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.ApplicationDoc
{
    [ExcludeFromCodeCoverage]
    public class OtherApplicationValidator : ApplicationValidatorBase<Application>
    {
        public OtherApplicationValidator()
        {
            RuleFor(x => x.ApplicationType)
                .Must(value => Enum.IsDefined(typeof(Enums.AppType), value))
                .WithMessage(ValidatorProvider.GetAppearsToHaveInvalidValueMsg("Application Type"));
            When(x => x.ApplicationType != Enums.AppType.DIS && x.ApplicationType != Enums.AppType.COA, () =>
            {
                //RuleFor(x => x.AttachmentRef)
                //    .NotEmpty()
                //    .WithMessage(ValidatorProvider.GetMustHaveUniqueAttachmentIdMsg(nameof(Document.AttachmentRef)));

                RuleFor(x => x.AttachmentId)
                    .NotEmpty()
                    .WithMessage(ValidatorProvider.GetMustBeGreaterThanOneMsg(nameof(Document.AttachmentId)));
            });
            //When(x => (x.ApplicationType == ApplicationTypes.DIS || x.ApplicationType == ApplicationTypes.COA) && x.CopyType != null, () =>
            //{
            //    RuleFor(x => x.AttachmentRef)
            //        .NotEmpty()
            //        .WithMessage(ValidatorProvider.GetMustHaveUniqueAttachmentIdMsg(nameof(Document.AttachmentRef)));
            //});
        }
    }
}
