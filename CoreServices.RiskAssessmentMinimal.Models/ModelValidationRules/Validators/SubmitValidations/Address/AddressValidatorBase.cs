using FluentValidation;
using  Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.Address
{
    [ExcludeFromCodeCoverage]
    public class AddressValidatorBase<TAddress> : AbstractValidator<TAddress> where TAddress : DTO.SubmitDtos.Types.Address

    {
        public AddressValidatorBase()
        {
            RuleFor(x => x.AddressType)
                .Must(value => Enum.IsDefined(typeof(MailAddress.Type),value))
                .WithMessage(a => ValidatorProvider.GetAppearsToHaveInvalidValueMsg($"Address Type {a.AddressType}"));
        }
    }
}
