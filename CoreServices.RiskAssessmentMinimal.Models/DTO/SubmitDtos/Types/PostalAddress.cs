using System.Runtime.Serialization;
using Edrs.ActionListenerService.Models.Common;
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.Address;
using Edrs.ActionListenerService.Models.ModelValidationRules;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(PostalMailAddressValidator))]
    public class PostalAddress : MailAddress
    {

        [RestrictedCharacterField(Constants.NonBlankTextTypeRegex)]
        [DataMember(IsRequired = true)]
        public string AddressLine1 { get; set; }
        [RestrictedCharacterField(Constants.NonBlankTextTypeRegex)]
        public string AddressLine2 { get; set; }
        [RestrictedCharacterField(Constants.NonBlankTextTypeRegex)]
        public string AddressLine3 { get; set; }
        [RestrictedCharacterField(Constants.NonBlankTextTypeRegex)]
        public string AddressLine4 { get; set; }
        [RestrictedCharacterField(Constants.NonBlankTextTypeRegex)]
        public string City { get; set; }
        [RestrictedCharacterField(Constants.NonBlankTextTypeRegex)]
        public string County { get; set; }
        [RestrictedCharacterField(Constants.NonBlankTextTypeRegex)]
        public string Country { get; set; }
        [RestrictedCharacterField(Constants.NonBlankTextTypeRegex)]
        public string Postcode { get; set; }

        public PostalAddress() : base(Type.Postal)
        {
        }
    }
}
