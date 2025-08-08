using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    public abstract class TitleNumbers
    {
        public new const string JsonNameOfTypeProperty = "titleNumbersType";
        public const string TitleNumberCharacterRestrictionRegex = "^[A-Z]{0,3}[0-9]{1,6}[ZT]?$";

        [JsonConverter(typeof(StringEnumConverter))]
        public enum Type
        {
            Dealing = 1,
            TransferOfPart = 2,
            NewLease = 3,
            LeaseExtension = 4
        }

        [DataMember(IsRequired = true)]
        [RestrictedCharacterField(TitleNumberCharacterRestrictionRegex)]
        public string[] Titles { get; set; }

        [DataValidation]
        [DataMember(IsRequired = true)]
        public Type TitleNumbersType { get; set; }

        public TitleNumbers(Type type)
        {
            TitleNumbersType = type;
        }
    }
}
