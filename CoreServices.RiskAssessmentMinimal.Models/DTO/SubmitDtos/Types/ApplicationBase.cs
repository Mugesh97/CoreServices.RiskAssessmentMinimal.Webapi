using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    public class ApplicationBase : Document
    {
        public const string JsonNameOfTypeProperty = "generalType";

        [JsonConverter(typeof(StringEnumConverter))]
        /// <summary>
        /// General Type
        /// </summary>
        public enum Type
        {
            Charge = 1,
            Other = 2
        }

        [DataMember(IsRequired = true)]
        public string ApplicationId { get; set; }

        [DataMember(IsRequired = true)]
        public string Value { get; set; }

        [DataMember(IsRequired = true)]
        public long FeeInPence { get; set; }

        [ValueValidation()]
        [DataMember(IsRequired = true)]
        public Type GeneralType { get; set; }

        public ApplicationBase(Type generalType)
        {
            GeneralType = generalType;
        }
    }
}
