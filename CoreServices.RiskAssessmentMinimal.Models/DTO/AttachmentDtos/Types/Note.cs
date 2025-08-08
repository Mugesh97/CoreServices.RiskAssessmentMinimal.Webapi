using Edrs.ActionListenerService.Models.Common;
using Edrs.ActionListenerService.Models.ModelValidationRules;
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.AttachmentValidations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Edrs.ActionListenerService.Models.DTO.AttachmentDtos.Types
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(NoteValidator))]
    public class Note : Item
    {
        [DataMember(IsRequired = true)]
        [RestrictedCharacterField(Constants.NonBlankTextTypeRegex)]
        public string Notes { get; set; }

        public Note() : base(Type.Note)
        {
        }
    }
}
