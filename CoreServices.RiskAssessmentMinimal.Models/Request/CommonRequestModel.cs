using Edrs.ActionListenerService.Models.DTO;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.Request
{
    [ExcludeFromCodeCoverage]
    public class CommonRequestModel : BaseDto
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? EdrsServiceType { get; set; }
        public string? ConveyancerId { get; set; }
        public string? CallbackUrl { get; set; }
        public string? LandmarkCallerId { get; set; }
        public string? TraceId { get; set; }
    }
}
