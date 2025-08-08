using Newtonsoft.Json;
using System.Runtime.Serialization;
using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Converters;
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.ApplicationParty;
using Edrs.ActionListenerService.Models.ModelValidationRules;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(ApplicationPartyValidator))]
    public class ApplicationParty
    {
        [DataValidation()]
        [JsonConverter(typeof(PartyConverter))]
        [DataMember(IsRequired = true)]
        public Party Party { get; set; }

        [DataMember(IsRequired = true)]
        public bool IsApplicant { get; set; }

        [DataValidation()]
        [JsonConverter(typeof(AddressForDocumentsConverter))]
        [DataMember(IsRequired = false)]
        public AddressForDocuments CorrespondenceAddress { get; set; }


        public ApplicationParty()
        {
        }

        public ApplicationParty(Party party)
        {
            Party = party;
        }

        public ApplicationParty(Party party, bool isApplicant) : this(party)
        {
            IsApplicant = isApplicant;
        }

        public ApplicationParty(Party party, bool isApplicant, AddressForDocuments correspondenceAddress) : this(party, isApplicant)
        {
            CorrespondenceAddress = correspondenceAddress;
        }
    }
}
