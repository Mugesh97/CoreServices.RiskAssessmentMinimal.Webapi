using System;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models
{
    [ExcludeFromCodeCoverage]
    public class RequestTrackingData
    {
        public Guid HmlrRequestId { get; set; }
        public string? Endpoint { get; set; }
        public string? EdrsCaseId { get; set; }
        public string? RequestStatus { get; set; }
        public string? HmlrUserName { get; set; }
        public string? DocumentId { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? LandmarkCallerId { get; set; }

    }
}
