
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.ServiceAddress;
using Edrs.ActionListenerService.Models.ModelValidationRules;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(TransferOrAssentAddressForDocsValidator))]
    public class TransferOrAssentAddress : AddressForDocuments
    {
        public TransferOrAssentAddress() : base(Type.TransferOrAssentAddress)
        {
        }
    }
}
