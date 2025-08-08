using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.TitleNumber;
using Edrs.ActionListenerService.Models.ModelValidationRules;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(NewLeaseTitleNumbersValidator))]
    public class NewLeaseTitleNumbers : TitleNumbers
    {
        [DataMember(IsRequired = false)]
        [RestrictedCharacterField(TitleNumberCharacterRestrictionRegex)]
        public string[] AdditionalTitles { get; set; }

        public NewLeaseTitleNumbers() : base(Type.NewLease)
        { }
    }
}
