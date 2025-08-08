
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.ServiceAddress;
using Edrs.ActionListenerService.Models.ModelValidationRules;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(SubjectPropertyAddressForDocsValidator))]
    public class SubjectPropertyAddress : AddressForDocuments
    {
        public SubjectPropertyAddress() : base(Type.SubjectPropertyAddress)
        {
        }
    }
}
