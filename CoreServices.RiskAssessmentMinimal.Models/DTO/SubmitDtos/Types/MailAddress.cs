using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    public abstract class MailAddress : Address
    {
        public new const string JsonNameOfTypeProperty = "mailAddressType";

        [JsonConverter(typeof(StringEnumConverter))]
        public new enum Type
        {
            Postal = 1,
            DX = 2
        }

        [DataValidation]
        [DataMember(IsRequired = true)]
        public Type MailAddressType { get; set; }

        public MailAddress(Type type) : base(Address.Type.Mail)
        {
            MailAddressType = type;
        }
    }
}
