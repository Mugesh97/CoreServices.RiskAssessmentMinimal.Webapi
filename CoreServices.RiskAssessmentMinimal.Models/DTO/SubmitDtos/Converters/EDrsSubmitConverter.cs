using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Converters
{
    [ExcludeFromCodeCoverage]
    public abstract class EDRSSubmitConverter<T> : JsonConverter
    {
        public override bool CanWrite => false;
        public override bool CanRead => true;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(T);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new InvalidOperationException("Use default serialization.");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            var isAnObjectArray = reader.TokenType == JsonToken.StartArray;
            if (isAnObjectArray)
            {
                var jsonArray = JArray.Load(reader);

                var objectArray = jsonArray.Select(json => GetOwnInstance().ReadJson(json.CreateReader(), typeof(T), existingValue, serializer)).Cast<T>().ToArray();
                return objectArray;
            }

            var jsonObject = JObject.Load(reader);
            return GetPopulatedObject(jsonObject, serializer, objectType, existingValue);
        }

        protected abstract EDRSSubmitConverter<T> GetOwnInstance();
        protected abstract object GetPopulatedObject(JObject jsonObject, JsonSerializer serializer, Type objectType, object existingValue);

        protected static T As<T>(JToken token)
        {
            if (token == null)
            {
                return default(T);
            }

            return (T)Convert.ChangeType(token, typeof(T));
        }
    }
}
