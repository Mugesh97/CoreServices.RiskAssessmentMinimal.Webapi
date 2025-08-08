using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    public abstract class Address
    {
        public const string JsonNameOfTypeProperty = "addressType";

        [JsonConverter(typeof(StringEnumConverter))]
        public enum Type
        {
            Email = 1,
            Mail = 2
        }

        [ValueValidation]
        [DataMember(IsRequired = true)]
        public Type AddressType { get; set; }

        public Address(Type type)
        {
            AddressType = type;
        }
    }
}
