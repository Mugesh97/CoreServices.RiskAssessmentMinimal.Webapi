using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.Notification
{
    [ExcludeFromCodeCoverage]
    public class AdditionalPartyNotificationValidator : AbstractValidator<AdditionalPartyNotification>
    {
        public AdditionalPartyNotificationValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetMustBeSetMsg("Name"));

            RuleFor(x => x.Reference)
                .NotEmpty()
                .When(l => l.Reference != null) // can be null based on HMLR schema
                .WithMessage(ValidatorProvider.GetMustBeSetMsg("Reference"));

            RuleFor(x => x.Address)
            .NotNull()
                .WithMessage(ValidatorProvider.GetMustBeSetMsg("Address"))
                .SetValidator(ValidatorProvider.GetPolymorphicValidators<DTO.SubmitDtos.Types.Address>());
        }
    }
}
