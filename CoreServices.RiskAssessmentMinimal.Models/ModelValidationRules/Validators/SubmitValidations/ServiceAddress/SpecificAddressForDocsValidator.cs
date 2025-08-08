using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.ServiceAddress
{
    [ExcludeFromCodeCoverage]
    public class SpecificAddressForDocsValidator : AddressForDocumentsValidatorBase<SpecificAddress>
    {
        public SpecificAddressForDocsValidator()
        {
            RuleFor(x => x.PostalAddress)
                .NotNull()
            .SetValidator(ValidatorProvider.GetExactTypeValidator<PostalAddress>());

            RuleFor(x => x.AdditionalAddress1)
                .SetValidator(ValidatorProvider.GetPolymorphicValidators<DTO.SubmitDtos.Types.Address>());

            RuleFor(x => x.AdditionalAddress2)
                .SetValidator(ValidatorProvider.GetPolymorphicValidators<DTO.SubmitDtos.Types.Address>());
        }
    }
}
