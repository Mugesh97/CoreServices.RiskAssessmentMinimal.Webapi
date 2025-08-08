using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;


namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Converters
{
    [ExcludeFromCodeCoverage]
    public class BoolValidatorConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            // Ensure the type to convert is bool
            return objectType == typeof(bool);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Boolean)
            {
                return (bool)reader.Value!;
            }

            throw new JsonSerializationException($"Invalid value for boolean type at {reader.Path}: {reader.Value}");
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value is bool boolValue)
            {
                writer.WriteValue(boolValue);
            }
            else
            {
                throw new JsonSerializationException("Expected a boolean value.");
            }
        }

    }
}
