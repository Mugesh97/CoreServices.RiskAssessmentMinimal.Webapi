using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics.CodeAnalysis;
//using Core.Services.Libraries.Base.Enums;

namespace Edrs.ActionListenerService.Models.Common
{
    [ExcludeFromCodeCoverage]
    public class HealthDetailResponse
    {
        /// <summary>
        /// online or offline
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ServiceStatus Status { get; set; }

        /// <summary>
        /// Version number of service
        /// </summary>
        public string Version { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public enum ServiceStatus
    {
        Online
    }
}
