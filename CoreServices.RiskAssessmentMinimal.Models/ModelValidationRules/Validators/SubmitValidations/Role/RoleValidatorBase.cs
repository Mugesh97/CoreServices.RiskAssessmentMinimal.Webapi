using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.Role
{
    [ExcludeFromCodeCoverage]
    public class RoleValidatorBase<TRole> : AbstractValidator<TRole> where TRole : DTO.SubmitDtos.Types.Role
    {
        public RoleValidatorBase()
        {
        }
    }
}
