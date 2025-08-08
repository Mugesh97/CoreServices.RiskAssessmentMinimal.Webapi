using System.Runtime.Serialization;
using Newtonsoft.Json;
using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Converters;
using Edrs.ActionListenerService.Models.ModelValidationRules;
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// Other conveyancer in the process, representing parties 
    /// not represented by the submitting conveyancer.
    /// 
    /// (E.g., in a case of house purchase, the buyer would be represented 
    /// by the submitting conveyancer and the lender (=the bank) would 
    /// have a different one.)
    /// </summary>

    [Validator(typeof(OtherConveyancerValidator))]
    public class OtherConveyancer
    {
        [DataValidation()]
        [DataMember(IsRequired = true)]
        public ApplicationParty[] Representees { get; set; }
        [DataMember(IsRequired = true)]
        public string Reference { get; set; }
        [DataMember(IsRequired = true)]
        public string Name { get; set; }
        [DataValidation()]
        [JsonConverter(typeof(MailAddressConverter))]
        [DataMember(IsRequired = true)]
        public MailAddress Address { get; set; }
    }
}

