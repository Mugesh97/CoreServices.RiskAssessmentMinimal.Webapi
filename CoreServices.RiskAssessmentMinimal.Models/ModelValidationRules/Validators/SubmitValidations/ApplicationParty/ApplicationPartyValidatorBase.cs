using Edrs.ActionListenerService.Models.Common;
using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.ApplicationParty
{
    [ExcludeFromCodeCoverage]
    public class ApplicationPartyValidatorBase<TApplicationParty> : AbstractValidator<TApplicationParty> where TApplicationParty : DTO.SubmitDtos.Types.ApplicationParty
    {
        public ApplicationPartyValidatorBase()
        {
            RuleFor(x => x.Party)
                .NotNull()
                .WithMessage(ValidatorProvider.GetMustBeSetMsg("Party"))
                .SetValidator(ValidatorProvider.GetPolymorphicValidators<DTO.SubmitDtos.Types.Party>());

            When(x => x.Party?.Roles != null && x.Party.Roles.Any(r => r.Type.Equals(RoleTypes.Transferee)), () =>
            {
                RuleFor(x => x.CorrespondenceAddress)
                    .NotNull()
                    .WithMessage($"{ValidatorProvider.GetMustBeSetMsg("Correspondence Address")} on all {RoleTypes.Transferee} parties");
            });

            RuleFor(x => x.CorrespondenceAddress)
                .SetValidator(ValidatorProvider.GetPolymorphicValidators<AddressForDocuments>());
        }
    }
}
