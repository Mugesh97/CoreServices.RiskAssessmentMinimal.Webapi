using CoreServices.RequestValidation.Nuget.Settings;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace CoreServices.RiskAssessmentMinimal.Webapi.Configuration.Models
{
    [ExcludeFromCodeCoverage]
    public class SchemaValidationSettings : IRequestValidationSettings, IAppSettings
    {

        public SchemaValidationSettings()
        {
            Version = GetVersion();
        }
        public AuthenticationSettings AuthenticationSettings { get; set; }

        public string Mode { get; set; }
        public string? Version { get; }
        public string Environment { get; set; } = null!;

        public string NewtonSoftSchemaKey { get; set; }
        public OpenApiDocument? OpenApiDocument { get; set; }

        public string OpenApiDocumentName { get; set; }

        public string OpenApiDocumentPath => $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}{Path.DirectorySeparatorChar}OpenApiStorage{Path.DirectorySeparatorChar}";

        public string PermissionOpenApiAttribute { get; set; }

        public string ExcludedPaths { get; set; }

        public bool ThrowExceptionOnFailure { get; set; }

        public bool ThrowAnExceptionOnSchemaValidationError => throw new NotImplementedException();

        IAuthenticationSettings IRequestValidationSettings.AuthenticationSettings => AuthenticationSettings;

        private static string? GetVersion() =>
            typeof(SchemaValidationSettings)
                .GetTypeInfo().Assembly
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
    }
}
