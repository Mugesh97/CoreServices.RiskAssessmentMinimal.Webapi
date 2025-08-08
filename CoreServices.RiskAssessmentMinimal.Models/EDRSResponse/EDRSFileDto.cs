using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.EDRSResponse
{
    [ExcludeFromCodeCoverage]
    public class EDRSFileDto
    {
        public string? Filename { get; set; }
        public string? Format { get; set; }
        public byte[]? Data { get; set; }
    }
}
