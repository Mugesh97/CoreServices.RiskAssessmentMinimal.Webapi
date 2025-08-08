using Core.Services.Libraries.WebApi.Web.Filters;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace CoreServices.RiskAssessmentMinimal.Webapi.Configuration.Swagger
{
    [ExcludeFromCodeCoverage]
    public static class Configuration
    {
        public static WebApplicationBuilder SwaggerConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.SwaggerConfiguration(builder.Configuration);
            return builder;
        }

        public static IServiceCollection SwaggerConfiguration(this IServiceCollection services, IConfigurationRoot config)
        {
            services.AddSwaggerGen(options =>
            {
                options.MapType<string>(() => new OpenApiSchema { Type = "string" }); // Optional: Handles string query parameters
                options.DescribeAllParametersInCamelCase();
                options.UseInlineDefinitionsForEnums();
                options.SchemaFilter<SwaggerEnumSchemaFilter>();
            });

            return services;
        }
    }
}
