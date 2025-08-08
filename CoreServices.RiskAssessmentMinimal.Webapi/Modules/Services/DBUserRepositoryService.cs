using CoreServices.RiskAssessmentMinimal.Implementation.DataBase;
using CoreServices.RiskAssessmentMinimal.Implementation.Models;
using CoreServices.RiskAssessmentMinimal.Webapi.Modules.Services.Interfaces;
using System.Net;


namespace CoreServices.RiskAssessmentMinimal.Webapi.Modules.Services
{
    public class DBUserRepositoryService(ILogger<DBUserRepositoryService> logger, IUserNotificationsRepository requestRepository): IDBUserRepositoryService
    {
        private readonly IUserNotificationsRepository _requestsRepository =
            requestRepository ?? throw new ArgumentNullException(nameof(requestRepository));

        private readonly ILogger<DBUserRepositoryService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public async Task<Guid> CreateNotification(UserNotification notification)
        {
            try
            {
                var id = await _requestsRepository.CreateNotificationAsync(notification);
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating notification for user {UserId}", notification.UserId);
                return Guid.Empty;
            }
        }

        public async Task<List<UserNotification>> GetUserNotifications(Guid userId)
        {
            try
            {
                return await _requestsRepository.GetNotificationsAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching notifications for user {UserId}", userId);
                throw; // Let the caller handle the exception if needed
            }
        }

        public async Task DeleteUserNotifications(Guid userId, Guid[] notificationIds)
        {
            try
            {
                await _requestsRepository.DeleteNotificationsAsync(userId, notificationIds);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting notifications for user {UserId}", userId);
                throw; // Rethrow to maintain method contract
            }
        }


    }
}
