using Microsoft.OpenApi.Models;

namespace CoreServices.RiskAssessmentMinimal.Webapi.Configuration.Models
{
    public interface IAppSettings
    {
        string? Version { get; }
        string OpenApiDocumentName { get; }
        string Environment { get; }
        string OpenApiDocumentPath { get; }
        OpenApiDocument? OpenApiDocument { get; set; }
        bool ThrowAnExceptionOnSchemaValidationError { get; }
    }
}
