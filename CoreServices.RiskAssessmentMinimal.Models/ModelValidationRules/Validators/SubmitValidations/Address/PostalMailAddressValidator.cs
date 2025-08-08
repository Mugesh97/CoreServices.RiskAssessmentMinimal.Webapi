using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.Address
{
    [ExcludeFromCodeCoverage]
    public class PostalMailAddressValidator : MailAddressValidatorBase<PostalAddress>
    {
        public PostalMailAddressValidator()
        {
            RuleFor(x => x.AddressLine1)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetCantBeBlankTextValueMsg("AddressLine1"));
        }
    }
}
