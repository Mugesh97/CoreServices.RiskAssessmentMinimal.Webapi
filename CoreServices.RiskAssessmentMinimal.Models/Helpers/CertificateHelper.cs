using Edrs.ActionListenerService.Models.Common;
using Edrs.ActionListenerService.Models.Helpers.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Kubernetes;
using VaultSharp.V1.Commons;


namespace Edrs.ActionListenerService.Models.Helpers
{
    [ExcludeFromCodeCoverage]
    public class CertificateHelper : ICertificateHelper
    {
        const string DefaultTokenPath = "/var/run/my-short-token/token";
        private readonly ILogger<CertificateHelper> _logger;
        private IAppSetting _appSetting;

        public CertificateHelper(ILogger<CertificateHelper> logger, IAppSetting appSetting)
        {
            // Initialize the logger directly in the constructor
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _appSetting = appSetting;
        }

        public X509Certificate2 GetCertificate(string thumbprint)
        {
            var certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            certStore.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection x509Certificate2Collection = certStore.Certificates.Find(
                X509FindType.FindByThumbprint, thumbprint, false);
            //get the first cert in the collection
            var cert = new X509Certificate2();
            if (x509Certificate2Collection.Count > 0)
            {
                cert = x509Certificate2Collection[0];

            }
            else
            {
                certStore.Close();
                return null;
            }
            certStore.Close();
            return cert;
        }


        public X509Certificate2 GetCertificate()
        {
            try
            {
                var vaultAddr = _appSetting.HmlrCertificate.Vault_Addr;

                if (String.IsNullOrEmpty(vaultAddr))
                {
                    throw new ArgumentNullException("Vault Address not found");
                }
                var roleName = _appSetting.HmlrCertificate.Vault_Role;

                if (String.IsNullOrEmpty(roleName))
                {
                    throw new ArgumentNullException("Vault Role Name not found");
                }
                //Get the path to service account token or fall back on default path


                string pathToToken = String.IsNullOrEmpty(_appSetting.HmlrCertificate.Sa_Token_Path)
                    ? DefaultTokenPath : _appSetting.HmlrCertificate.Sa_Token_Path;


                string jwt = File.ReadAllText(pathToToken);
                _logger.LogInformation("{ClassName}-{MethodName} - jwt: {jwt}", "CertificateHelper", "GetCertificate", jwt);

                IAuthMethodInfo authMethod = new KubernetesAuthMethodInfo(roleName, jwt);
                var vaultClientSettings = new VaultClientSettings(vaultAddr, authMethod);
                IVaultClient vaultClient = new VaultClient(vaultClientSettings);

                // We can retrieve the secret after creating our VaultClient object
                Secret<SecretData> kv2Secret = null;
                var secretPath = _appSetting.HmlrCertificate.Vault_Path;
                var secretMountPoint = _appSetting.HmlrCertificate.Vault_Mount;
                kv2Secret = vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(path: secretPath, mountPoint: secretMountPoint).Result;

                var certificate = kv2Secret.Data.Data[_appSetting.HmlrCertificate.CertifcateSecretName];
                var password = kv2Secret.Data.Data[_appSetting.HmlrCertificate.CertifcatePasswordSecretName];

                byte[] certificateBytes = Convert.FromBase64String(certificate!.ToString());
                var cert = new X509Certificate2(certificateBytes, password!.ToString());

                var intermediateCertificateString = kv2Secret.Data.Data[_appSetting.HmlrCertificate.HmlrIssuingCA];
                var rootCertificateString = kv2Secret.Data.Data[_appSetting.HmlrCertificate.HmlrRootCA];

                byte[] intermediateCertificateBytes = Convert.FromBase64String(intermediateCertificateString!.ToString());
                byte[] rootCertificateBytes = Convert.FromBase64String(rootCertificateString!.ToString());

                var intermediateCertificate = new X509Certificate2(intermediateCertificateBytes);
                var rootCertificate = new X509Certificate2(rootCertificateBytes);

                X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadWrite);
                store.Add(rootCertificate);

                X509Store store2 = new X509Store(StoreName.CertificateAuthority, StoreLocation.CurrentUser);
                store2.Open(OpenFlags.ReadWrite);
                store2.Add(intermediateCertificate);

                var certificateChain = new X509Chain();
                certificateChain.ChainPolicy.ExtraStore.Add(intermediateCertificate);
                certificateChain.ChainPolicy.ExtraStore.Add(rootCertificate);
                certificateChain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck; // Disable revocation checks

                if (!certificateChain.Build(cert))
                {
                    _logger.LogError("Certificate chain validation failed. Detailed chain status follows:");

                    foreach (var status in certificateChain.ChainStatus)
                    {
                        _logger.LogError("{ClassName}-{MethodName} - ChainStatus: {Status}, Information: {StatusInformation}",
                            "CertificateHelper", "GetCertificate", status.Status, status.StatusInformation);
                    }

                    throw new Exception("Certificate chain validation failed.");
                }

                return cert;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{ClassName} - {MethodName} - exception occurred in kv2Secret for CaseId : {CaseId}", "CertificateHelper", "GetCertificate", ex.Message);
                return null;
            }
        }
    }
}
