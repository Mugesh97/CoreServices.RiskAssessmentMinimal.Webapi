using Edrs.ActionListenerService.Models.Common;
using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using Edrs.ActionListenerService.Models.Request;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.AttachmentValidations
{
    [ExcludeFromCodeCoverage]
    public class SubmitRequestValidator : AbstractValidator<SubmitRequest>
    {
        public SubmitRequestValidator()
        {
            RuleFor(x => x.CaseId)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetMustBeSetMsg("Case Id"));

            RuleFor(x => x.CaseManagementSystemReference)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetMustBeSetMsg("Case Management System Reference"))
                .WithMessage(ValidatorProvider.GetAppearsToHaveInvalidValueMsg("Case Management System Reference"));

            RuleFor(x => x.TotalFeeInPence)
                .Matches(Constants.NonNegativeIntegerTypeRegex)
                .WithMessage(ValidatorProvider.GetMustBeGreaterThanOrEqualToZeroMsg("Total Fee In Pence"));

            RuleFor(x => x.TotalFeeInPence)
               .NotNull()
               .WithMessage(ValidatorProvider.GetMustBeSetMsg("TotalFeeInPence Not Null"));

            RuleFor(x => x.ApplicationDate)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetMustBeSetMsg("Application Date"));

            RuleFor(x => x.TitleNumbers)
                .NotNull()
                .WithMessage(ValidatorProvider.GetMustBeSetMsg("Title Numbers"))
                .SetValidator(ValidatorProvider.GetPolymorphicValidators<TitleNumbers>());

            RuleFor(x => x.TitleNumbers.Titles)
               .NotNull()
               .WithMessage(ValidatorProvider.GetMustBeSetMsg("Titles within Title Numbers"));


            RuleFor(x => x.SubmittingConveyancer)
                .NotNull()
                .WithMessage(ValidatorProvider.GetMustBeSetMsg("Submitting Conveyancer"))
                .SetValidator(ValidatorProvider.GetExactTypeValidator<SubmittingConveyancer>());


            RuleForEach(x => x.OtherConveyancers)
                .NotNull()
                .WithMessage(ValidatorProvider.GetMustBeSetMsg("Conveyancer"))
                .SetValidator(ValidatorProvider.GetExactTypeValidator<OtherConveyancer>());

            RuleFor(x => x.UnrepresentedParties)
                .SetValidator(ValidatorProvider.GetExactTypeValidator<UnrepresentedParties>());

            RuleForEach(x => x.SupportingDocuments)
                .NotNull()
                .WithMessage(ValidatorProvider.GetMustBeSetMsg("Supporting Document"))
                .SetValidator(ValidatorProvider.GetExactTypeValidator<SupportingDocument>());


            RuleForEach(x => x.AdditionalPartyNotifications)
                .SetValidator(ValidatorProvider.GetExactTypeValidator<AdditionalPartyNotification>());

            RuleForEach(x => x.Applications)
                .ChildRules(app =>
                {
                    app.RuleFor(a => a.ApplicationId)
                   .NotEmpty()
                   .NotNull()
                   .WithMessage(ValidatorProvider.GetMustBeSetMsg("ApplicationId must be set."));
                });

            RuleForEach(x => x.Applications)
                .ChildRules(app =>
                {
                    app.RuleFor(a => a.Value)
                   .NotEmpty()
                   .NotNull()
                   .WithMessage(ValidatorProvider.GetMustBeSetMsg("Value must be set."));
                });

            RuleForEach(x => x.Applications)
                .ChildRules(app =>
                {
                    app.RuleFor(a => a.FeeInPence)
                   .NotEmpty()
                   .NotNull()
                   .WithMessage(ValidatorProvider.GetMustBeSetMsg("FeeInPence must be set."))
                   .GreaterThanOrEqualTo(0)
                   .WithMessage(ValidatorProvider.GetMustBeSetMsg("FeeInPence must be zero or a positive value."));
                });

        }
    }
}
