using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Converters;
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.ServiceAddress;
using Edrs.ActionListenerService.Models.ModelValidationRules;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;


namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(SpecificAddressForDocsValidator))]
    public class SpecificAddress : AddressForDocuments
    {
        [DataValidation()]
        [DataMember(IsRequired = true)]
        public PostalAddress PostalAddress { get; set; }
        [DataValidation()]
        [JsonConverter(typeof(AddressConverter))]
        [DataMember(IsRequired = false)]
        public Address AdditionalAddress1 { get; set; }
        [DataValidation()]
        [JsonConverter(typeof(AddressConverter))]
        [DataMember(IsRequired = false)]
        public Address AdditionalAddress2 { get; set; }

        public SpecificAddress() : base(Type.SpecificAddress)
        {
        }
    }
}
