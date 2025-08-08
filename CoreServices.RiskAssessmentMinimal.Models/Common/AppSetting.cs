using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.Common
{
    [ExcludeFromCodeCoverage]
    public class AppSetting : IAppSetting
    {
        private readonly IConfiguration _configuration;

        public AppSetting(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            // Bind all settings sections to their respective properties
            RabbitMq = new RabbitMqSettings();
            _configuration.GetSection("RabbitMqSettings").Bind(RabbitMq);

            HmlrCertificate = new HmlrCertificateSettings();
            _configuration.GetSection("HmlrCertificateSettings").Bind(HmlrCertificate);

            Polly = new PollySettings();
            _configuration.GetSection("PollySettings").Bind(Polly);

            HmlrEndPoint = new HmlrEndPointSettings();
            _configuration.GetSection("HmlrEndPointSettings").Bind(HmlrEndPoint);

            EnvironmentSettings = new EnvironmentSettings();
            _configuration.GetSection("EnvironmentSettings").Bind(EnvironmentSettings);
        }
        public RabbitMqSettings RabbitMq { get; }
        public HmlrCertificateSettings HmlrCertificate { get; }
        public PollySettings Polly { get; }
        public HmlrEndPointSettings HmlrEndPoint { get; }
        public EnvironmentSettings EnvironmentSettings { get; }
    }

    [ExcludeFromCodeCoverage]
    public class RabbitMqSettings
    {
        public virtual string? HostName { get; set; }
        public virtual string? UserName { get; set; }
        public virtual string? Password { get; set; }
        public virtual string? Queue { get; set; }
        public virtual string? Port { get; set; }
        public string? DeadLetterQueue { get; set; }
        public string? DeadLetterExchange { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class HmlrCertificateSettings
    {
        public string? CertifcateSecretName { get; set; }
        public string? CertifcatePasswordSecretName { get; set; }
        public string? HmlrIssuingCA { get; set; }
        public string? HmlrRootCA { get; set; }
        public string? Vault_Role { get; set; }
        public string? Vault_Addr { get; set; }
        public string? Sa_Token_Path { get; set; }
        public string? Vault_Path { get; set; }
        public string? Vault_Mount { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class PollySettings
    {
        public virtual string? RetryPolicyCount { get; set; }
        public virtual string? RetryPolicyWaitingTimeInMilliseconds { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class HmlrEndPointSettings
    {
        public virtual string? HmlrEdrsBaseUrl { get; set; }
        public string? CertificateThumbPrint { get; set; }
        public string? HmlrSubmitCaseWebService { get; set; }
        public virtual string? HmlrAttachmentCaseWebService { get; set; }
        public virtual string? EcssServiceAppId { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class EnvironmentSettings
    {
        public virtual string? Environment { get; set; }
    }
}
