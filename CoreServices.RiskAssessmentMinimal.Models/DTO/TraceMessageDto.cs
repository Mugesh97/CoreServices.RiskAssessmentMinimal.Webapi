using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.DTO
{
    [ExcludeFromCodeCoverage]
    public class TraceMessageDto<T>
    {
        public T? Payload { get; set; }
        public string? OperationID { get; set; }
    }
}
