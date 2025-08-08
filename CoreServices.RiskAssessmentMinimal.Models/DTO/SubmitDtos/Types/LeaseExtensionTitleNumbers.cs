using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.TitleNumber;
using Edrs.ActionListenerService.Models.ModelValidationRules;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(LeaseExtensionTitleNumbersValidator))]
    public class LeaseExtensionTitleNumbers : TitleNumbers
    {
        [DataMember(IsRequired = false)]
        [RestrictedCharacterField(TitleNumberCharacterRestrictionRegex)]
        public string[] AdditionalTitles { get; set; }

        [DataMember(IsRequired = true)]
        [RestrictedCharacterField(TitleNumberCharacterRestrictionRegex)]
        public string LesseeTitle { get; set; }

        public LeaseExtensionTitleNumbers() : base(Type.LeaseExtension)
        { }
    }
}
