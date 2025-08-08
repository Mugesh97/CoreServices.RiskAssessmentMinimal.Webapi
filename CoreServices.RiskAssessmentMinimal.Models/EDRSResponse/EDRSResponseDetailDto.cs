using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Edrs.ActionListenerService.Models.EDRSResponse
{
    [ExcludeFromCodeCoverage]
    public class EDRSResponseDetailDto
    {
        public string? ErrorMessage { get; set; }
        public string? CaseId { get; set; }
        public string? AttachmentId { get; set; }
        public string? PollStatus { get; set; }
        public string? PollStatusMessage { get; set; }
        public HttpStatusCode? StatusCode { get; set; }
        public DateTime DelayUntil { get; set; }
        public decimal Cost { get; set; }

        public int Fee { get; set; }

        public EDRSFileDto? File { get; set; }
        public string? Abr { get; set; }
    }
}
