using System.Runtime.Serialization;
using Edrs.ActionListenerService.Models.Common;
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.Address;
using Edrs.ActionListenerService.Models.ModelValidationRules;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(DXMailAddressValidator))]
    public class DXAddress : MailAddress
    {
        public DXAddress() : base(Type.DX)
        {
        }

        [RestrictedCharacterField(Constants.NonBlankTextTypeRegex)]
        [DataMember(IsRequired = true)]
        public string DXNumber { get; set; }
        [RestrictedCharacterField(Constants.NonBlankTextTypeRegex)]
        [DataMember(IsRequired = true)]
        public string DXExchange { get; set; }
    }
}
