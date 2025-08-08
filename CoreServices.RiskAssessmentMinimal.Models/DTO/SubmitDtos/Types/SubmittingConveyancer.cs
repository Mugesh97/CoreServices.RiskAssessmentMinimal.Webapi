using Edrs.ActionListenerService.Models.ModelValidationRules.Validators;
using Edrs.ActionListenerService.Models.ModelValidationRules;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// Conveyancer submitting the application to the Land Registry 
    /// = can be referred also as the 'acting conveyancer' or 
    /// the lodging conveyancer.
    /// 
    /// (
    ///     This is the actual user submitting the application. 
    /// 
    ///     Land Registry identifies the user based on his credentials. 
    ///     He had to register with them prior to this. They already have 
    ///     his details - that's why only the 'variable' items are submitted.
    /// )
    /// </summary>
    /// 
    [Validator(typeof(SubmittingConveyancerValidator))]
    public class SubmittingConveyancer
    {
        public const string CaseReferenceRestrictionRegex = @"^(?=.*\S.*)(?=^.{1,25}$)";
        public const string NonBlankTextTypeRegex = @".*\S.*";

        [DataValidation()]
        [DataMember(IsRequired = true)]
        public ApplicationParty[] Representees { get; set; }
        [DataMember(IsRequired = true)]
        [RestrictedCharacterField(CaseReferenceRestrictionRegex)]
        public string CaseReference { get; set; }
        [DataMember(IsRequired = true)]
        [RestrictedCharacterField(NonBlankTextTypeRegex)]
        public string Email { get; set; }
        [DataMember(IsRequired = true)]
        [RestrictedCharacterField(NonBlankTextTypeRegex)]
        public string PhoneNo { get; set; }
    }
}
