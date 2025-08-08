using Edrs.ActionListenerService.Models.DTO.AttachmentDtos.Types;
using Edrs.ActionListenerService.Models.DTO.AttachmentDtos.Types.IdentifierTypes;
using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Converters;
using Edrs.ActionListenerService.Models.Exceptions;
using Edrs.ActionListenerService.Models.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Edrs.ActionListenerService.Models.DTO.AttachmentDtos.Converters
{
    [ExcludeFromCodeCoverage]
    public class IdentifierConverter : EDRSSubmitConverter<Identifier>
    {
        protected override EDRSSubmitConverter<Identifier> GetOwnInstance()
        {
            return new IdentifierConverter();
        }

        protected override object GetPopulatedObject(JObject jsonObject, JsonSerializer serializer, Type objectType, object existingValue)
        {
            //Get exception type
            var invalidDtoException = new InvalidEDRSAttachmentParametersException();

            var identifier = default(Identifier);

            var jsonProperty = jsonObject.GetValue(Identifier.JsonNameOfTypeProperty, StringComparison.OrdinalIgnoreCase);

            if (jsonProperty == null) return identifier;

            try
            {
                //Add validate identifier method here, pre-assignment - to catch case sensitivity
                if (!EnumHelper.IsDefinedCaseInsensitive(typeof(Identifier.Type), jsonProperty.ToString()))
                {
                    throw invalidDtoException;
                }
            }
            catch (Exception ex)
            {
                var identifierProperty = jsonProperty.Path;

                const string constraint = "AttachmentId, Application or DocumentName";

                invalidDtoException.DtoValidationMessage = $" - {identifierProperty} is not valid with respect to validation constraint: {constraint}";
                throw invalidDtoException;
            }

            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (As<Identifier.Type>(jsonProperty))
            {
                case Identifier.Type.Application:
                    identifier = new ApplicationIdentifier();
                    identifier.ValidateValue(GetJsonValue(jsonObject, "Value"));
                    break;

                case Identifier.Type.AttachmentId:
                    identifier = new AttachmentIdIdentifier();
                    identifier.ValidateValue(GetJsonValue(jsonObject, "Value"));
                    break;

                case Identifier.Type.DocumentName:
                    identifier = new DocumentNameIdentifier();
                    identifier.ValidateValue(GetJsonValue(jsonObject, "Value"));
                    break;
            }

            serializer.Populate(jsonObject.CreateReader(), identifier);

            return identifier;
        }

        // Helper method to handle both "Value" and "value"
        private static string GetJsonValue(JObject jsonObject, string key)
        {
            // Attempt case-insensitive lookup
            var token = jsonObject.Properties()
                                   .FirstOrDefault(p => string.Equals(p.Name, key, StringComparison.OrdinalIgnoreCase))?
                                   .Value;

            if (token == null)
            {
                throw new KeyNotFoundException($"Key '{key}' or a case-insensitive equivalent not found in the JSON object.");
            }

            return token.ToString();
        }
    }
}
