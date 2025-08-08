using Core.Services.Libraries.WebApi.Web.Startup.ExceptionHandler;
using Core.Services.Libraries.WebApi.Web.Startup.Json;
using Core.Services.Libraries.WebApi.Web.Startup.Logging;
using Core.Services.Libraries.WebApi.Web.Startup.Modularization;
using CoreServices.RiskAssessmentMinimal.Implementation.DataBase;
using CoreServices.RiskAssessmentMinimal.Webapi.Configuration.DependencyInjection;
using CoreServices.RiskAssessmentMinimal.Webapi.Configuration.Swagger;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: ExcludeFromCodeCoverage]
var builder = WebApplication.CreateBuilder(args);


builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();


builder.DependencyInjection();
builder.SwaggerConfiguration();
builder.SerilogConfiguration();

builder.Services.AddEndpointsApiExplorer();
builder.Services.JsonConfiguration();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IDbConnectionManager, DbConnectionManager>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("PostgresConnection");
    return new DbConnectionManager(connectionString);

});
builder.Services.AddScoped<IUserNotificationsRepository, DbUserNotificationsRepository>();

builder.Services.RegisterModules(Assembly.GetExecutingAssembly());




var appInsightsConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"];

builder.Services.AddSingleton<ILoggerFactory, LoggerFactory>();


if (!string.IsNullOrEmpty(appInsightsConnectionString))
{
    builder.Logging.AddApplicationInsights(
        configureTelemetryConfiguration: (config) =>
            config.ConnectionString = appInsightsConnectionString,
        configureApplicationInsightsLoggerOptions: (options) => { }
    );
}

var app = builder.Build();


if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseRequestBodyLogging();
app.UseResponseBodyLogging();
app.UseCustomExceptionHandler();
app.UseStatusCodePageHandler();
app.UseHttpsRedirection();
app.MapEndpoints();

app.Run();
