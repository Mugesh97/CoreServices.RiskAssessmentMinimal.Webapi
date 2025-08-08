using CoreServices.RiskAssessmentMinimal.Implementation.Models.Enum;
using System.Diagnostics.CodeAnalysis;

namespace CoreServices.RiskAssessmentMinimal.Implementation.Models
{
    [ExcludeFromCodeCoverage]
    public class UserNotification
    {
        public Guid? NotificationId { get; set; } 
        public required UserNotificationType NotificationType { get; set; }
        public string? NotificationText { get; set; }
        public required Guid UserId { get; set; }
        public bool IsRead { get; set; }
        public DateTimeOffset? Timestamp { get; set; }

        public UserNotification() { }

        [SetsRequiredMembers]
        public UserNotification(UserNotificationTableEntity tableEntity)
        {
            NotificationId = tableEntity.NotificationId;
            UserId = tableEntity.UserId;
            NotificationType = tableEntity.NotificationType;
            Timestamp = tableEntity.Timestamp;
            IsRead = tableEntity.IsRead;
            NotificationText = tableEntity.NotificationText;
        }
    }
}
