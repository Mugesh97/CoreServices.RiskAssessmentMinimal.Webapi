using CoreServices.RiskAssessmentMinimal.Webapi.Modules.Services.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace CoreServices.RiskAssessmentMinimal.Webapi.Modules.Services
{
    [ExcludeFromCodeCoverage]
    public class ApplicationVersionService : IApplicationVersion
    {
        public string Version { get; }

        public ApplicationVersionService()
        {
            Version = GetVersion();
        }

        private static string GetVersion()
        {
            return typeof(ApplicationVersionService)
                        .GetTypeInfo().Assembly
                        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? string.Empty;
        }
    }
}
