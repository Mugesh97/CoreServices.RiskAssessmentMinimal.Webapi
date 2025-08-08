using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using FluentValidation;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.ServiceAddress
{
    [ExcludeFromCodeCoverage]
    public class AddressForDocumentsValidatorBase<TAddresssForDocs> : AbstractValidator<TAddresssForDocs> where TAddresssForDocs : AddressForDocuments
    {
        public AddressForDocumentsValidatorBase()
        {
            RuleFor(x => x.AddressForDocumentsType)
                .Must(value => Enum.IsDefined(typeof(AddressForDocuments.Type),value))
                .WithMessage(a => ValidatorProvider.GetAppearsToHaveInvalidValueMsg($"Address For Documents Type {a.AddressForDocumentsType}"));
        }
    }
}
