using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.Address;
using Edrs.ActionListenerService.Models.ModelValidationRules;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(EmailAddressValidator))]
    public class EmailAddress : Address
    {
        [DataMember(IsRequired = true)]
        public string Email { get; set; }

        public EmailAddress() : base(Type.Email)
        {
        }
    }
}