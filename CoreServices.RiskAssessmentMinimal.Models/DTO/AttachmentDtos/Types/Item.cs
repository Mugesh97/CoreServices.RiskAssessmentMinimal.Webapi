using Edrs.ActionListenerService.Models.ModelValidationRules;
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.AttachmentValidations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Edrs.ActionListenerService.Models.DTO.AttachmentDtos.Types
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(ItemValidator))]
    public abstract class Item
    {
        public const string JsonNameOfTypeProperty = "itemType";
        [JsonConverter(typeof(StringEnumConverter))]
        public enum Type
        {
            Attachment = 1,
            Note = 2
        }

        [ValueValidation()]
        [DataMember(IsRequired = true)]
        public Type ItemType { get; set; }

        public Item(Type type)
        {
            this.ItemType = type;
        }

        public virtual void ValidateValue(string value)
        {
            //Do type method validation
        }
    }
}
