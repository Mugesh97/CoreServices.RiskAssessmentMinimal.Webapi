using CoreServices.RiskAssessmentMinimal.Implementation.Models;

namespace CoreServices.RiskAssessmentMinimal.Webapi.Modules.Services.Interfaces
{
    public interface IDBUserRepositoryService
    {
        public Task<Guid> CreateNotification(UserNotification notification);
        public Task<List<UserNotification>> GetUserNotifications(Guid userId);
        public Task DeleteUserNotifications(Guid userId, Guid[] notificationIds);

    }
}
