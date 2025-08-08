using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.ServiceModel;

namespace Edrs.ActionListenerService.Models.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class HttpHelper
    {
        private static readonly HttpClient HttpClient;
        private const int MaxRetryAttempts = 3;
        private const int PauseBetweenFailuresInMillis = 50;
        private static readonly TimeSpan PauseBetweenFailures = TimeSpan.FromMilliseconds(PauseBetweenFailuresInMillis);

        static HttpHelper()
        {
            HttpClient = new HttpClient();
        }

        public static BasicHttpBinding GetBasicHttpBinding()
        {
            var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            binding.MaxReceivedMessageSize = int.MaxValue;
            binding.MaxBufferSize = int.MaxValue;
            binding.Security = new BasicHttpSecurity
            {
                Mode = BasicHttpSecurityMode.Transport, // Or None, if you don't require SSL
                Transport = new HttpTransportSecurity
                {
                    ClientCredentialType = HttpClientCredentialType.Certificate // or None, if no credentials are needed
                }
            };
            return binding;
        }
    }
}
