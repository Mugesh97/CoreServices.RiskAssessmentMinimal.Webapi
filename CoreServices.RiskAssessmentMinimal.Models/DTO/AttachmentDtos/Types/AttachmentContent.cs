using Edrs.ActionListenerService.Models.ModelValidationRules;
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.AttachmentValidations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Edrs.ActionListenerService.Models.DTO.AttachmentDtos.Types
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(AttachmentContentValidator))]
    public class AttachmentContent
    {
        public string Name { get; set; }

        [DataMember(IsRequired = true)]
        public string Data { get; set; }
    }
}
