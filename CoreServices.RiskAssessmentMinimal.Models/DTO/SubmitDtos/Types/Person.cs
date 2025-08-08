using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.Party;
using Edrs.ActionListenerService.Models.ModelValidationRules;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(PersonPartyValidator))]
    public class Person : Party
    {
        [DataMember(IsRequired = true)]
        public string Forename { get; set; }
        [DataMember(IsRequired = true)]
        public string Surname { get; set; }
    }
}
