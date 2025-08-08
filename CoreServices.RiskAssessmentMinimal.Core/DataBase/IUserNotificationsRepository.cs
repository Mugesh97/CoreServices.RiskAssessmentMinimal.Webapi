using CoreServices.RiskAssessmentMinimal.Implementation.Models;

namespace CoreServices.RiskAssessmentMinimal.Implementation.DataBase
{
    public interface IUserNotificationsRepository
    {
        public Task<Guid> CreateNotificationAsync(UserNotification notification);
        public Task<List<UserNotification>> GetNotificationsAsync(Guid userId);
        public Task DeleteNotificationsAsync(Guid userId, Guid[] notificationIds);
    }
}
