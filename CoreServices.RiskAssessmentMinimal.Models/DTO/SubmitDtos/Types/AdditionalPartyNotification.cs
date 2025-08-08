using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Converters;
using Edrs.ActionListenerService.Models.Common;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.Notification;
using Edrs.ActionListenerService.Models.ModelValidationRules;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(AdditionalPartyNotificationValidator))]
    public class AdditionalPartyNotification
    {
        [DataMember(IsRequired = true)]
        public string Name { get; set; }

        [DataMember(IsRequired = false)]
        [RestrictedCharacterField(Constants.NonBlankTextTypeRegex)]
        public string Reference { get; set; }

        [DataValidation()]
        [JsonConverter(typeof(AddressConverter))]
        [DataMember(IsRequired = true)]
        public Address Address { get; set; }
    }
}
