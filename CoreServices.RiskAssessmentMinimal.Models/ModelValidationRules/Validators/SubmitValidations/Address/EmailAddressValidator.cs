using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.Address
{
    [ExcludeFromCodeCoverage]
    public class EmailAddressValidator : AddressValidatorBase<EmailAddress>
    {
        public EmailAddressValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetCantBeBlankTextValueMsg("Email Address"));
        }
    }
}
