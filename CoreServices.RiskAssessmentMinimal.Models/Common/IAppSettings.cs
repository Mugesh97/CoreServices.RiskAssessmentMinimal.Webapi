namespace Edrs.ActionListenerService.Models.Common
{
    public interface IAppSetting
    {
        RabbitMqSettings RabbitMq { get; }
        HmlrCertificateSettings HmlrCertificate { get; }
        PollySettings Polly { get; }
        HmlrEndPointSettings HmlrEndPoint { get; }
        EnvironmentSettings EnvironmentSettings { get; }
    }
}