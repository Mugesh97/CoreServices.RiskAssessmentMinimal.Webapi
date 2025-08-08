using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Converters;
using Edrs.ActionListenerService.Models.ModelValidationRules;
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.ApplicationParty;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(PartyVerifiedByIdFormValidator))]
    public class PartyVerifiedByIdForm : ApplicationParty
    {
        [DataValidation]
        [DataMember(IsRequired = true)]
        [JsonConverter(typeof(IdentityFormConverter))]
        public IdentityForm IdentityForm { get; set; }
    }
}
