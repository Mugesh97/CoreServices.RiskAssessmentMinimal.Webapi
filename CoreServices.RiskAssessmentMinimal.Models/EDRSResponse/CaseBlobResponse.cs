using System;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.EDRSResponse
{
    [ExcludeFromCodeCoverage]
    public class CaseBlobResponse
    {
        public Guid UniqueGuidId { get; set; }
        public string? CaseId { get; set; }
        public string? ServiceType { get; set; }
        public string? JsonBlob { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }

}
