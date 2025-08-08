using Core.Services.Libraries.WebApi.Web.Filters;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System.Diagnostics.CodeAnalysis;

namespace CoreServices.RiskAssessmentMinimal.Webapi.TelemetryProcessors
{
    public class RequestTelemetryFilter(ITelemetryProcessor next, IList<LogExclusionFilter> exclusionFilters) : ITelemetryProcessor
    {
        private readonly ITelemetryProcessor _next = next ?? throw new ArgumentNullException(nameof(next));
        private readonly IList<LogExclusionFilter> _exclusionFilters = exclusionFilters ?? throw new ArgumentNullException(nameof(exclusionFilters));

        public void Process(ITelemetry item)
        {
            if (item is RequestTelemetry requestTelemetry)
            {
                foreach (var filter in _exclusionFilters)
                {
                    if (ShouldExclude(requestTelemetry, filter))
                    {
                        return;
                    }
                }
            }

            _next.Process(item);
        }

        private static bool ShouldExclude(RequestTelemetry requestTelemetry, LogExclusionFilter filter)
        {
            if (requestTelemetry.Url == null)
            {
                return false;
            }

            var propertyValue = filter.Property switch
            {
                "RequestPath" => requestTelemetry.Url.PathAndQuery,
                _ => string.Empty
            };

            return filter.Condition switch
            {
                FilterCondition.EndsWith => propertyValue.EndsWith(filter.Criteria, StringComparison.OrdinalIgnoreCase),
                FilterCondition.StartsWith => propertyValue.StartsWith(filter.Criteria, StringComparison.OrdinalIgnoreCase),
                FilterCondition.Equals => propertyValue.Equals(filter.Criteria, StringComparison.OrdinalIgnoreCase),
                FilterCondition.Contains => propertyValue.Contains(filter.Criteria, StringComparison.OrdinalIgnoreCase),
                _ => false
            };
        }
    }
}
