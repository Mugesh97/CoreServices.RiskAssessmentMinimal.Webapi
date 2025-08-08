using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoreServices.RiskAssessmentMinimal.Implementation.Models.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum UserNotificationType
    {
        RequestAssignedToUserHasBeenCancelled,
        RequestSentBackToUserForReassessment,
        ReassessedRequestReadyForUserToReview
    }
}
