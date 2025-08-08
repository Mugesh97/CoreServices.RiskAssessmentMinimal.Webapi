
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.EDRSResponse
{
    [ExcludeFromCodeCoverage]
    public class CallbackDto : IUserDetails
    {
        public EDRSResponseDto Response;
        public string? ConveyancerId { get; set; }
        public string Message { get; set; }
        public string MessageId { get; set; }
        public string RequestId { get; set; }
        public string UserName { get; set; }
        public string CallbackUrl { get; set; }
    }
}
