using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.DTO
{
    [ExcludeFromCodeCoverage]
    public class CredentialsDto
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
