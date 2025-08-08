//using Core.Services.Libraries.Base.Enums;
using Core.Services.Libraries.Base.Exceptions;
using Core.Services.Libraries.WebApi.Web.Models;
using Core.Services.Libraries.WebApi.Web.Startup.Modularization.Interfaces;
using CoreServices.RiskAssessmentMinimal.Webapi.Modules.Services.Interfaces;
using CoreServices.RiskAssessmentMinimal.Webapi.Modules.Services;
using System.Diagnostics.CodeAnalysis;

namespace CoreServices.RiskAssessmentMinimal.Webapi.Modules
{
    [ExcludeFromCodeCoverage]
    public class Health : BaseModule, IModule
    {
        #region protected fields

        private readonly ILogger _logger;

        protected enum Endpoints
        {
            lbHealth,
            information,
            health
        }

        #endregion

        #region constructor

        public Health(IConfiguration config, ILogger<Health> logger)
           : base(config)
        {
            _logger = logger;
        }

        #endregion

        #region registration

        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            services.AddScoped<IApplicationVersion, ApplicationVersionService>();
            return services;
        }

        #endregion

        #region endpoint mapping 

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            EndpointConfig<Endpoints>? config = _config?.GetSection(nameof(EndpointConfig))?.Get<EndpointConfig<Endpoints>>();

            if (config is null)
                throw new MissingConfigException(nameof(EndpointConfig));

            EndpointMeta<Endpoints> endpoint = config.Get(Endpoints.lbHealth);

            endpoints.MapMethods(config.GetPath(endpoint), endpoint.Verbs, () => { return Results.Ok(); })
                    .WithTags(endpoint.Tag)
                    .WithName(endpoint.OperationId)
                    .Produces(StatusCodes.Status200OK)
                    .Produces(StatusCodes.Status429TooManyRequests)
                    .AllowAnonymous();

            endpoint = config.Get(Endpoints.health);
            endpoints.MapMethods(config.GetPath(endpoint), endpoint.Verbs, () => { return Results.Ok(); })
                    .WithTags(endpoint.Tag)
                    .WithName(endpoint.OperationId)
                    .Produces(StatusCodes.Status200OK)
                    .Produces(StatusCodes.Status429TooManyRequests)
                    .AllowAnonymous();

            return endpoints;
        }

        #endregion

    }
}
