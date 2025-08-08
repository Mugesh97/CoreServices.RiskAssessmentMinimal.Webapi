using System.Diagnostics.CodeAnalysis;

namespace CoreServices.RiskAssessmentMinimal.Webapi.TelemetryProcessors
{
    [ExcludeFromCodeCoverage]
    public static class TelemetryExtensions
    {
        public static IServiceCollection AddCustomTelemetry(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddApplicationInsightsTelemetry(options =>
            //    {
            //        options.EnableAdaptiveSampling = false;

            //    }
            //);
            //services.AddApplicationInsightsTelemetryProcessor<AddCustomPropertiesTelemetryProcessor>();

            return services;
        }
    }
}
