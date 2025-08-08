using Edrs.ActionListenerService.Models.DTO;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.EDRSResponse
{
    [ExcludeFromCodeCoverage]
    public class EDRSResponseDto
    {
        public EDRSResponseDto(string type, EDRSResponseDetailDto content, string serviceType)
        {
            Type = type;
            Content = content;
            ServiceType = serviceType;
        }

        public string Type { get; set; }
        public EDRSResponseDetailDto Content { get; set; }
        public string ServiceType { get; set; }
        public string? Message { get; set; }
        public LogEventsDto? LogEvents { get; set; }
    }

}
