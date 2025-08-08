using System.Security.Cryptography.X509Certificates;

namespace Edrs.ActionListenerService.Models.Helpers.Interface
{
    public interface ICertificateHelper
    {
        X509Certificate2 GetCertificate();
        X509Certificate2 GetCertificate(string thumbprint);
    }
}