using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using FluentValidation;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.Address
{
    [ExcludeFromCodeCoverage]
    public class MailAddressValidatorBase<TMailAddress> : AddressValidatorBase<TMailAddress> where TMailAddress : DTO.SubmitDtos.Types.MailAddress
    {
        public MailAddressValidatorBase()
        {
            RuleFor(x => x.MailAddressType)
                .Must(value => Enum.IsDefined(typeof(MailAddress.Type), value))
                .WithMessage(a => ValidatorProvider.GetAppearsToHaveInvalidValueMsg($"Mail Address Type {a.MailAddressType}"));
        }
    }
}
