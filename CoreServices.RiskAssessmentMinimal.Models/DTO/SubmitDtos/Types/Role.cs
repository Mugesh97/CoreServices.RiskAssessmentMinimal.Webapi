using System.Runtime.Serialization;
using Edrs.ActionListenerService.Models.Helpers;
using Edrs.ActionListenerService.Models.ModelValidationRules;
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.Role;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Edrs.ActionListenerService.ConnectedServices.EDRSSubmit;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(RoleValidator))]
    public class Role
    {
        [DataMember(IsRequired = true)]
        public string ApplicationId { get; set; }

        [ValueValidation]
        [DataMember(IsRequired = true)]
        public RoleType Type { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum RoleType
        {
            [EDRSEnum(RoleTypeContent.Borrower)]
            Borrower = 1,
            [EDRSEnum(RoleTypeContent.Lender)]
            Lender = 2,
            [EDRSEnum(RoleTypeContent.PersonalRepresentative)]
            PersonalRepresentative = 3,
            [EDRSEnum(RoleTypeContent.Proprietor)]
            Proprietor = 4,
            [EDRSEnum(RoleTypeContent.ThirdParty)]
            ThirdParty = 5,
            [EDRSEnum(RoleTypeContent.Transferee)]
            Transferee = 6,
            [EDRSEnum(RoleTypeContent.Transferor)]
            Transferor = 7,
            [EDRSEnum(RoleTypeContent.Lessee)]
            Lessee = 8,
            [EDRSEnum(RoleTypeContent.Lessor)]
            Lessor = 9,
            [EDRSEnum(RoleTypeContent.PowerOfAttorney)]
            PowerOfAttorney = 10,
        }
    }
}
