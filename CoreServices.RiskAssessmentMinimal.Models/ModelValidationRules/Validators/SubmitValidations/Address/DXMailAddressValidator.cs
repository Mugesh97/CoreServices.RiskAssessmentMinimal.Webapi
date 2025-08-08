using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.Address
{
    [ExcludeFromCodeCoverage]
    public class DXMailAddressValidator : MailAddressValidatorBase<DXAddress>
    {
        public DXMailAddressValidator()
        {
            RuleFor(x => x.DXNumber)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetCantBeBlankTextValueMsg("DX Number"));

            RuleFor(x => x.DXExchange)
                .NotEmpty()
                .WithMessage(ValidatorProvider.GetCantBeBlankTextValueMsg("DX Exchange"));
        }
    }
}
