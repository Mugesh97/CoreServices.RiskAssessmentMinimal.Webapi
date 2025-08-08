using CoreServices.RiskAssessmentMinimal.Implementation.Models;
using CoreServices.RiskAssessmentMinimal.Implementation.Models.Enum;
using Microsoft.Extensions.Logging;
using Npgsql;
using NpgsqlTypes;

namespace CoreServices.RiskAssessmentMinimal.Implementation.DataBase
{
    public class DbUserNotificationsRepository : IUserNotificationsRepository
    {
        private readonly IDbConnectionManager _connectionManager;
        private readonly ILogger<DbUserNotificationsRepository> _logger;
        const string className = "DbUserNotificationsRepository";

        public DbUserNotificationsRepository(IDbConnectionManager connectionManager, ILogger<DbUserNotificationsRepository> logger)
        {
            _connectionManager = connectionManager;
            _logger = logger;
        }

        public async Task<Guid> CreateNotificationAsync(UserNotification notification)
        {
            const string methodName = nameof(CreateNotificationAsync);
            const string query = @"
             INSERT INTO riskassessment.usernotifications
            (notificationid, user_id, notification_type, notification_text, is_read, createddate)
            VALUES 
            (@NotificationId, @UserId, @NotificationType, @NotificationText, @IsRead, @Timestamp)
            RETURNING notificationid;";


            return await ExecuteDatabaseOperationAsync(query, notification, methodName);
        }


        public async Task<List<UserNotification>> GetNotificationsAsync(Guid userId)
        {
            const string methodName = nameof(GetNotificationsAsync);
            const string query = @"
        SELECT notificationid, user_id, notification_type, notification_text, is_read, createddate
        FROM riskassessment.usernotifications
        WHERE user_id = @UserId";

            var notifications = new List<UserNotification>();
            var parameter = new NpgsqlParameter("UserId", userId);

            try
            {
                await _connectionManager.OpenConnectionAsync();

                using var reader = await _connectionManager.ExecuteReaderAsync(query, parameter);
                while (await reader.ReadAsync())
                {
                    notifications.Add(new UserNotification
                    {
                        NotificationId = reader.GetGuid(0),
                        UserId = reader.GetGuid(1),
                        NotificationType = Enum.TryParse<UserNotificationType>(reader.GetString(2), out var type) ? type : default,
                        NotificationText = reader.IsDbNull(3) ? null : reader.GetString(3),
                        IsRead = !reader.IsDbNull(4) && reader.GetBoolean(4),
                        Timestamp = reader.IsDbNull(5) ? DateTime.MinValue : reader.GetDateTime(5)
                    });

                }

                return notifications;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Class} - {Method} - exception occurred for UserId: {UserId}",
                    nameof(className), methodName, userId);
                return new List<UserNotification>();
            }
            finally
            {
                await _connectionManager.CloseConnectionAsync();
            }
        }

        public async Task DeleteNotificationsAsync(Guid userId, Guid[] notificationIds)
        {
            const string query = @"
        DELETE FROM riskassessment.usernotifications
        WHERE user_id = @userId AND notificationId = ANY(@notificationIds)";

            try
            {
                var parameters = new[]
                {
            new NpgsqlParameter("userId", NpgsqlDbType.Uuid) { Value = userId },
            new NpgsqlParameter("notificationIds", NpgsqlDbType.Array | NpgsqlDbType.Uuid) { Value = notificationIds }
        };

                await _connectionManager.OpenConnectionAsync();

                await _connectionManager.ExecuteNonQueryAsync(query, parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting notifications for UserId: {UserId}", userId);
                throw; // Propagate the exception to be handled by the caller
            }
            finally
            {
                await _connectionManager.CloseConnectionAsync();
            }
        }


        private async Task<Guid> ExecuteDatabaseOperationAsync(string query, UserNotification request, string methodName)
        {
            try
            {
                var parameters = GetSqlParameters(request);

                await _connectionManager.OpenConnectionAsync();
                var result = await _connectionManager.ExecuteScalarAsync(query, parameters);

                if (result != null && Guid.TryParse(result.ToString(), out var notificationId))
                {
                    return notificationId;
                }
                else
                {
                    throw new InvalidOperationException("Failed to retrieve inserted NotificationId.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{ClassName} - {MethodName} - Exception occurred for UserId: {UserId}",
                    className, methodName, request.UserId);
                throw;
            }
            finally
            {
                await _connectionManager.CloseConnectionAsync();
            }
        }

        private static NpgsqlParameter[] GetSqlParameters(UserNotification notification)
        {
            return new[]
            {
        new NpgsqlParameter("NotificationId", notification.NotificationId ?? Guid.NewGuid()),
        new NpgsqlParameter("NotificationType", notification.NotificationType.ToString()),
        new NpgsqlParameter("NotificationText", notification.NotificationText ?? (object)DBNull.Value),
        new NpgsqlParameter("UserId", notification.UserId),
        new NpgsqlParameter("IsRead", notification.IsRead),
        new NpgsqlParameter("Timestamp", notification.Timestamp ?? (object)DBNull.Value)
    };
        }



    }
}
