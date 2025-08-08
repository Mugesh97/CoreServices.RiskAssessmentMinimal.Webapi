
using System.Diagnostics.CodeAnalysis;

namespace CoreServices.RiskAssessmentMinimal.Implementation.Models
{
    [ExcludeFromCodeCoverage]
    public class UserNotificationTableEntity : UserNotification
    {
        public UserNotificationTableEntity() {}

        [SetsRequiredMembers]
        public UserNotificationTableEntity(UserNotification userNotification)
        {
            userNotification.NotificationId ??= Guid.NewGuid();
            NotificationId = userNotification.NotificationId;
            UserId = userNotification.UserId;
            NotificationType = userNotification.NotificationType;
            IsRead = userNotification.IsRead;
            NotificationText = userNotification.NotificationText;
        }
    }
}
