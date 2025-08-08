using Edrs.ActionListenerService.Models.ModelValidationRules.Validators;
using Edrs.ActionListenerService.Models.ModelValidationRules;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;
namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(UnrepresentedPartiesValidator))]
    public class UnrepresentedParties
    {
        [DataValidation()]
        [DataMember(IsRequired = false)]
        public PartyVerifiedByIdForm[] PartiesVerifiedByIdForms { get; set; }

        /// <summary>
        /// Grouping legally-unrepresented parties, for which the 
        /// submitting conveyancer proclaims that their identity has been 
        /// verified by him (without attaching any documents).
        /// </summary> 
        [DataValidation()]
        [DataMember(IsRequired = false)]
        public ApplicationParty[] PartiesVerifiedWithoutIdForms { get; set; }
    }
}
