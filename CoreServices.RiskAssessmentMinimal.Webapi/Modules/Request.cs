using Core.Services.Libraries.Base.Exceptions;
using Core.Services.Libraries.WebApi.Web.Authorization.Models;
using Core.Services.Libraries.WebApi.Web.Models;
using Core.Services.Libraries.WebApi.Web.Startup.Modularization.Interfaces;
using System.Diagnostics.CodeAnalysis;
using CoreServices.RequestValidation.Nuget.Interfaces;
using CoreServices.RiskAssessmentMinimal.Webapi.Modules.Services;
using CoreServices.RiskAssessmentMinimal.Webapi.Modules.Services.Interfaces;
using Core.Services.Libraries.WebApi.Web.ExceptionHandler;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using CoreServices.RiskAssessmentMinimal.Webapi.Modules;
using CoreServices.RiskAssessmentMinimal.Implementation.Models;
using CoreServices.RiskAssessmentMinimal.Model;
using static CoreServices.RiskAssessmentMinimal.Webapi.Common.Constants;


namespace CoreServices.RiskAssessmentMinimal.Webapi.Modules
{
    [ExcludeFromCodeCoverage]
    public class Request : BaseModule, IModule
    {
        #region protected fields

        private readonly IOpenApiRepository _openApiRepository;
        private readonly IOpenApiService _openApiService;
        private readonly ISchemaValidationService _schemaValidationService;
        private readonly ILogger<Request> _logger;
        protected enum Endpoints
        {
            CreateNotification,
            GetUserNotification,
            DeleteNotification
        }

        #region private fields

        private const string InvalidCredentialMessage = "Invalid or missing credentials.";
        private const string EmptyOrNullMessage = "Request body is empty or null";
        private const string InvalidJsonPayloadMessage = "Invalid JSON payload";
        private const string InvalidLandmarkCallerId = "Invalid or missing LandmarkCallerId";

        #endregion

        #endregion

        #region constructor

        public Request(IConfiguration config, IOpenApiRepository openApiRepository, IOpenApiService openApiService, ISchemaValidationService schemaValidationService, ILogger<Request> logger)
            : base(config)
        {
            _openApiRepository = openApiRepository;
            _openApiService = openApiService;
            _schemaValidationService = schemaValidationService;
            _logger = logger;
        }

        #endregion

        #region registration

        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            services.AddScoped<IDBUserRepositoryService, DBUserRepositoryService>();
            return services;
        }

        #endregion

        #region endpoint mapping 

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            EndpointConfig<Endpoints>? config = _config?.GetSection(nameof(EndpointConfig))?.Get<EndpointConfig<Endpoints>>();

            if (config is null)
            {
                throw new MissingConfigException(nameof(EndpointConfig));
            }

            EndpointMeta<Endpoints> endpoint = config.Get(Endpoints.CreateNotification);

            endpoints.MapMethods(config.GetPath(endpoint), endpoint.Verbs, CreateNotification)
                   .WithTags(endpoint.Tag)
                   .WithName(endpoint.OperationId)
                   .Produces(StatusCodes.Status200OK)
                   .Produces(StatusCodes.Status429TooManyRequests)
                   .RequireAuthorization(nameof(LandmarkAuthorization));

            endpoint = config.Get(Endpoints.GetUserNotification);
            endpoints.MapMethods(config.GetPath(endpoint), endpoint.Verbs, GetUserNotifications)
                   .WithTags(endpoint.Tag)
                   .WithName(endpoint.OperationId)
                   .Produces(StatusCodes.Status200OK)
                   .Produces(StatusCodes.Status429TooManyRequests)
                   .RequireAuthorization(nameof(LandmarkAuthorization));

            endpoint = config.Get(Endpoints.DeleteNotification);
            endpoints.MapMethods(config.GetPath(endpoint), endpoint.Verbs, DeleteNotification)
                   .WithTags(endpoint.Tag)
                   .WithName(endpoint.OperationId)
                   .Produces(StatusCodes.Status200OK)
                   .Produces(StatusCodes.Status429TooManyRequests)
                   .RequireAuthorization(nameof(LandmarkAuthorization));

            return endpoints;
        }

        #endregion

        #region function handlers

        public async Task<IResult> CreateNotification(
            IDBUserRepositoryService dBUserRepositoryService,
            HttpRequest httpRequest,
            IHttpErrorHandler httpErrorHandler)
        {
            try
            {
                var notification = await httpRequest.ReadFromJsonAsync<UserNotification>();

                if (notification == null)
                {
                    return Results.BadRequest(new
                    {
                        type = "CreateUserNotification",
                        message = "Invalid request body",
                        data = (object?)null
                    });
                }

                var id = await dBUserRepositoryService.CreateNotification(notification);

                if (id == Guid.Empty)
                {
                    return Results.StatusCode(StatusCodes.Status500InternalServerError);
                }

                return Results.Ok(new
                {
                    type = "CreateUserNotification",
                    message = "Notification created successfully",
                    data = id
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user notification");
                return Results.Problem("An unexpected error occurred");
            }
        }

        public async Task<IResult> DeleteNotification(
            IDBUserRepositoryService dBUserRepositoryService,
            HttpRequest httpRequest,
            IHttpErrorHandler httpErrorHandler)
        {
            try
            {
                var headers = ExtractHeaders(httpRequest);
                _ = headers.TryGetValue(LandMarkCallerId, out string? callerId) ? callerId : string.Empty;

                if (string.IsNullOrWhiteSpace(callerId))
                {
                    _logger.LogWarning("DeleteNotification called without a valid userId");
                    return Results.BadRequest(new
                    {
                        type = "DeleteNotification",
                        message = "CaseId is missing or invalid",
                        data = (object?)null
                    });
                }

                UserNotificationRequest? body;
                using (var reader = new StreamReader(httpRequest.Body))
                {
                    var bodyString = await reader.ReadToEndAsync();
                    body = JsonSerializer.Deserialize<UserNotificationRequest>(bodyString);
                }

                if (body == null || body.NotificationIds == null || !body.NotificationIds.Any())
                {
                    _logger.LogWarning("DeleteNotification called without valid notificationIds");
                    return Results.BadRequest(new
                    {
                        type = "DeleteNotification",
                        message = "NotificationIds are missing or invalid",
                        data = (object?)null
                    });
                }

                // Convert array of string → array of Guid
                var notificationGuids = ConvertToGuidArray(body.NotificationIds);

                _logger.LogInformation("Calling DeleteNotification for UserId: {UserId}", callerId);

                await dBUserRepositoryService.DeleteUserNotifications(Guid.Parse(callerId), notificationGuids);

                _logger.LogInformation("DeleteNotification completed for UserId: {UserId}", callerId);

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                var error = $"Error occurred while processing DeleteNotification request: {ex.Message}";
                _logger.LogError(ex, "Exception in DeleteNotification: {Error}", error);

                return Results.Problem(
                detail: error,
                title: "DeleteNotification",
                statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<IResult> GetUserNotifications(
            IDBUserRepositoryService dBUserRepositoryService,
            HttpRequest httpRequest,
            IHttpErrorHandler httpErrorHandler)
        {
            try
            {
                var userId = httpRequest.Headers["X-Landmark-Caller-Id"].FirstOrDefault();

                if (string.IsNullOrWhiteSpace(userId))
                {
                    _logger.LogWarning("GetUserNotifications called without a valid userId");
                    return Results.BadRequest(new
                    {
                        type = "GetUserNotifications",
                        message = "userId is missing or invalid",
                        data = (object?)null
                    });
                }

                var notifications = await dBUserRepositoryService.GetUserNotifications(Guid.Parse(userId));

                return Results.Ok(new
                {
                    type = "GetUserNotifications",
                    message = "User notifications fetched successfully",
                    data = notifications
                });
            }
            catch (Exception ex)
            {
                var error = $"Error occurred while processing GetUserNotifications request: {ex.Message}";
                _logger.LogError(ex, "Exception in GetUserNotifications: {Error}", error);

                return Results.Problem(
                detail: error,
                title: "GetUserNotifications",
                statusCode: StatusCodes.Status500InternalServerError);
            }
        }


        #endregion

        #region Private Methods

        public static Guid[] ConvertToGuidArray(string[] stringArray)
        {
            return stringArray
                .Select(id => Guid.TryParse(id, out var guid) ? guid : Guid.Empty)
                .Where(guid => guid != Guid.Empty)
                .ToArray();
        }

        private static Dictionary<string, string> ExtractHeaders(HttpRequest httpRequest)
        {
            var headers = new Dictionary<string, string>();

            if (httpRequest.Headers != null)
            {
                foreach (var header in httpRequest.Headers)
                {
                    headers[header.Key.ToLower()] = header.Value.ToString();
                }
            }

            return headers;
        }

        #endregion
    }

}
