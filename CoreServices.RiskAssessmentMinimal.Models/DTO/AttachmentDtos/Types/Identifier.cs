using Edrs.ActionListenerService.Models.ModelValidationRules;
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.AttachmentValidations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Edrs.ActionListenerService.Models.DTO.AttachmentDtos.Types
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(IdentifierValidator))]
    public abstract class Identifier
    {
        public const string JsonNameOfTypeProperty = "identifierType";
        [JsonConverter(typeof(StringEnumConverter))]

        public enum Type
        {
            AttachmentId = 1,
            Application = 2,
            DocumentName = 3
        }

        [DataMember(IsRequired = true)]
        public Type IdentifierType { get; set; }

        public Identifier(Type type)
        {
            IdentifierType = type;
        }

        public virtual void ValidateValue(string value)
        {
            //Overwrite method within bespoke identifier
        }
    }
}