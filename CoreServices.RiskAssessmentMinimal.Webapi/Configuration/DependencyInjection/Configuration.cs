using Azure.Identity;
using Core.Services.Libraries.Base.Exceptions;
using Core.Services.Libraries.Http.Azure.AccessToken;
using Core.Services.Libraries.Http.Azure.AccessToken.Interfaces;
using Core.Services.Libraries.Http.Azure.Client;
using Core.Services.Libraries.Http.Azure.Client.Interfaces;
using Core.Services.Libraries.Http.Services.Authentication;
using Core.Services.Libraries.Http.Services.Interfaces;
using Core.Services.Libraries.Services.WebApi.Web.Authorization.Models;
using Core.Services.Libraries.WebApi.Web.Authorization;
using Core.Services.Libraries.WebApi.Web.Authorization.Interfaces;
using Core.Services.Libraries.WebApi.Web.Authorization.Middleware;
using Core.Services.Libraries.WebApi.Web.Authorization.Models;
using Core.Services.Libraries.WebApi.Web.Middleware;
using CoreServices.RequestValidation.Nuget.Extensions;
using CoreServices.RiskAssessmentMinimal.Webapi.Configuration.Models;
using CoreServices.RiskAssessmentMinimal.Webapi.SchemaValidation;
using CoreServices.RiskAssessmentMinimal.Webapi.TelemetryProcessors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using ExceptionHandler = Core.Services.Libraries.WebApi.Web.Startup.ExceptionHandler;

namespace CoreServices.RiskAssessmentMinimal.Webapi.Configuration.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class Configuration
    {
        public static WebApplicationBuilder DependencyInjection(this WebApplicationBuilder builder)
        {
            builder.Services.DependencyInjection(builder.Configuration);

            return builder;
        }

        public static IServiceCollection DependencyInjection(this IServiceCollection services, IConfigurationRoot config)
        {
            services.AddSingleton(config);
            services.AddApplicationInsightsTelemetry(options =>
            {
                options.EnableAdaptiveSampling = false;
            });
            services.AddCustomTelemetry(config);

            #region ea.azure nuget

            var azureAccessTokenServiceSettings = config
                .GetSection(nameof(AzureAccessTokenServiceSettings))
                .Get<AzureAccessTokenServiceSettings>();

            services.AddSingleton<IAzureAccessTokenServiceSettings>(azureAccessTokenServiceSettings ?? default!);

            services.AddScoped<IRestClient, RestClient>();
            services.AddScoped<IAzureAccessTokenService, AzureAccessTokenService>();
            services.AddScoped<IAzureAccessTokenProviderService, AzureAccessTokenProviderService>();
            #endregion

            #region schema validation setup

            // bootstrap the validation section
            var validationSettings = config
                .GetSection("SchemaValidationSettings")
                .Get<SchemaValidationSettings>();
            if (validationSettings == null) throw new MissingConfigException(nameof(SchemaValidationSettings));
            services.AddRequestValidationRegistrations(validationSettings);

            #endregion

            #region request and response body logging
            services.AddTransient<RequestBodyLoggingMiddleware>();
            services.AddTransient<ResponseBodyLoggingMiddleware>();
            #endregion


            var tokenValidatorSettings = config.GetSection(nameof(TokenValidatorSettings)).Get<TokenValidatorSettings>();
            services.AddSingleton<ITokenValidatorSettings>(tokenValidatorSettings ?? default!);
            services.AddTransient<ISecurityTokenValidator, JwtSecurityTokenHandler>();
            services.AddSingleton<IAzureConnectConfigManager>(new AzureConnectConfigManager($"{tokenValidatorSettings?.AzureActiveDirectoryIssuer}.well-known/openid-configuration", new OpenIdConnectConfigurationRetriever()));
            services.AddTransient<ITokenValidator, TokenValidator>();
            services.AddSingleton<IAuthorizationHandler, LandmarkAuthorizationHandler>();
            services.AddSingleton<IAuthorizationMiddlewareResultHandler, LandmarkAuthorizationMiddlewareResultHandler>();
            services.AddScoped<ISchemaValidationService, SchemaValidationService>();
            services.AddScoped<IValidationErrorsFormatter, ValidationErrorsFormatter>();
            services.AddAuthorization(o =>
            {
                o.AddPolicy(nameof(LandmarkAuthorization), p => p.AddRequirements(new LandmarkAuthorization()));
            });


            services.AddScoped<IAzureAccessTokenService, AzureAccessTokenService>();
            services.AddScoped<IAzureAccessTokenProviderService, AzureAccessTokenProviderService>();
            services.AddSingleton<DefaultAzureCredential>();

            #region Error handler
            ExceptionHandler.Configuration.ExceptionHandlerConfiguration(services);
            #endregion

            return services;
        }

        private static AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(new[] { TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2) });
        }
    }
}
