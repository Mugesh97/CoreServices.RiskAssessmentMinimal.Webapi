using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using static Edrs.ActionListenerService.Models.Common.Enums;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    public abstract class Document
    {
        [DataMember(IsRequired = true)]
        [NumberMinMaxValidation(1)]
        public int? AttachmentId { get; set; }

        [ValueValidation()]
        [DataMember(IsRequired = true)]
        public DocumentCopyType CopyType { get; set; }
    }
}


