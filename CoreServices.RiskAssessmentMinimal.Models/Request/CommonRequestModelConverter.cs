using Edrs.ActionListenerService.Models.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.Request
{
    [ExcludeFromCodeCoverage]
    public class CommonRequestModelConverter : JsonConverter<CommonRequestModel>
    {
        public override void WriteJson(JsonWriter writer, CommonRequestModel? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
        public override CommonRequestModel? ReadJson(JsonReader reader, Type objectType, CommonRequestModel? existingValue,
            bool hasExistingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);

            var edrsServiceType = jsonObject["Payload"]!["EdrsServiceType"]?.ToString();
            CommonRequestModel result;

            switch (edrsServiceType)
            {
                case Constants.EDRSATTACHMENTSERVICE:
                    result =  jsonObject["Payload"].ToObject<EDRSAttachmentDto>();                    
                    break;
                case Constants.EDRSSUBMITSERVICE:
                    result = jsonObject["Payload"].ToObject<SubmitRequest>();
                    break;
                default:
                    throw new JsonException("Unknown or missing EDRSServiceType.");
            }
            result!.TraceId = jsonObject["OperationID"]?.ToString();
            return result;
        }
    }
}
