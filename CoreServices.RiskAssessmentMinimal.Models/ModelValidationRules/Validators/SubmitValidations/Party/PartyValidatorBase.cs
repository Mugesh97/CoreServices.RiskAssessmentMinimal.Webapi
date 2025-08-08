using FluentValidation;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.Party
{
    [ExcludeFromCodeCoverage]
    public class PartyValidatorBase<TParty> : AbstractValidator<TParty> where TParty : DTO.SubmitDtos.Types.Party
    {
        public PartyValidatorBase()
        {
            RuleFor(x => x.PartyType)
                .Must(value => Enum.IsDefined(typeof(DTO.SubmitDtos.Types.Party.Type),value))
                .WithMessage(ValidatorProvider.GetAppearsToHaveInvalidValueMsg("Party Type"));
        }
    }
}
