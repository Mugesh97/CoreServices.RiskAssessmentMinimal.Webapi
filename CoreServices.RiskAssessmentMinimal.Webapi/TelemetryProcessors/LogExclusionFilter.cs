using System.Diagnostics.CodeAnalysis;

namespace CoreServices.RiskAssessmentMinimal.Webapi.TelemetryProcessors
{
    [ExcludeFromCodeCoverage]
    public class LogExclusionFilter
    {
        public string? Property { get; set; } = null;
        public FilterCondition Condition { get; set; }
        public string Criteria { get; set; } = null!;
    }

    public enum FilterCondition
    {
        Undefined,
        EndsWith,
        StartsWith,
        Equals,
        Contains
    }
}
