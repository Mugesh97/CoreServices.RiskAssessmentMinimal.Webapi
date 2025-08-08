using CoreServices.RiskAssessmentMinimal.Webapi.Configuration.Models;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using System.Diagnostics.CodeAnalysis;

namespace CoreServices.RiskAssessmentMinimal.Webapi.TelemetryProcessors
{
    public class AddCustomPropertiesTelemetryProcessor(ITelemetryProcessor next, IAppSettings schemaValidationSettings) : ITelemetryProcessor
    {
        private ITelemetryProcessor Next { get; set; } = next;
        private readonly IAppSettings _appSettings = schemaValidationSettings;

        public void Process(ITelemetry item)
        {
            // Add Custom properties
            item.Context.GlobalProperties["MachineName"] = Environment.MachineName;
            item.Context.GlobalProperties["BuildVersion"] = _appSettings.Version ?? "Unknown";

            this.Next.Process(item);
        }
    }
}
