using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Runtime.Serialization;
using Edrs.ActionListenerService.Models.Exceptions;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.DTO
{
    [ExcludeFromCodeCoverage]
    public class LogEventsDtoParameters
    {
        public string Endpoint { get; set; }
        public decimal? Cost { get; set; } = 0.0M;
        public string MessageId { get; set; }
        public JToken PostJsonBody { get; set; }
        public string Username { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class LogEventsDto : BaseDto
    {
        public LogEventsDto()
        {
            this.InvalidDtoException = new InvalidLogEventsParametersException();
            this.TimeStamp = DateTime.UtcNow;
        }

        [DataMember(IsRequired = true)]
        public string LegalEntity { get; set; }
        [DataMember(IsRequired = true)]
        public string Product { get; set; }
        [DataMember(IsRequired = true)]
        public string ApplicationName { get; set; }
        [DataMember(IsRequired = true)]
        public string Environment { get; set; }
        [DataMember(IsRequired = true)]
        public string SessionId { get; set; }
        [DataMember(IsRequired = true)]
        public string UserId { get; set; }
        [DataMember(IsRequired = false)]
        public string EventName { get; } = "HmlrBusinessGateway";
        [DataMember(IsRequired = false)]
        private DateTime TimeStamp { get; set; }
        [JsonIgnore]
        [DataMember(IsRequired = false)]
        public LogEventsDtoParameters Parameters { get; set; }

        [ExcludeFromCodeCoverage]
        public bool IsNotInitialised
        {
            get
            {
                return string.IsNullOrEmpty(LegalEntity) && string.IsNullOrEmpty(Product) && string.IsNullOrEmpty(ApplicationName) && string.IsNullOrEmpty(SessionId);
            }
        }

        [ExcludeFromCodeCoverage]
        public void SetCost(decimal cost)
        {
            this.Parameters.Cost = cost;
        }

        [ExcludeFromCodeCoverage]
        public void LateInitialiseLogEvent(LogEventsDto logEventsDto)
        {
            if (IsNotInitialised && logEventsDto != null)
            {
                LegalEntity = logEventsDto.LegalEntity;
                Product = logEventsDto.Product;
                ApplicationName = logEventsDto.ApplicationName;
                Environment = logEventsDto.Environment;
                SessionId = logEventsDto.SessionId;
                UserId = logEventsDto.UserId;

                Parameters = logEventsDto.Parameters;
            }
        }

        //public async Task<string> GetLogEventsPostBody()
        //{
        //    var jsonString = JsonConvert.SerializeObject(this, new JsonSerializerSettings
        //    {
        //        ContractResolver = new JsonIgnoreAttributeIgnorerContractResolver()
        //    });

        //    var obfuscatedJToken = await LoggingHelper.GetLogJToken("GetLogEventsPostBody", new List<string> { "Parameters.PostJsonBody.callbackUrl", "Parameters.PostJsonBody.eDRSAttachment.item.content.data" }, jsonString);
        //    return obfuscatedJToken.ToString();
        //}
    }

    [ExcludeFromCodeCoverage]
    public class JsonIgnoreAttributeIgnorerContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            property.Ignored = false;
            return property;
        }
    }
}
