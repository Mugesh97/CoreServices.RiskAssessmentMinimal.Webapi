using Edrs.ActionListenerService.Models.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Edrs.ActionListenerService.Models.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class LoggingHelper
    {
        public static bool IsInternalServerError(this System.Exception? ex)
        {
            return ex != null && 
                    ex.Message.Contains("The remote server returned an error: (500) Internal Server Error.", StringComparison.OrdinalIgnoreCase) ||
                    ex.Message.Contains("The content type application/json of the response message does not match the content type of the binding", StringComparison.OrdinalIgnoreCase) ||
                    ex.Message.Contains(Constants.EDRSNOENDPOINTLISTENING, StringComparison.OrdinalIgnoreCase) ||
                    ex.Message.Contains(Constants.EdrsInternalServerError, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsCredentialsInvalidError(this System.Exception? ex)
        {
            if (ex == null) return false;
            return ex.Message.Contains(Constants.EdrsAccessLoginInvalid, StringComparison.OrdinalIgnoreCase) ||
                   ex.Message.Contains(Constants.EdrsAccessPasswordNotValid, StringComparison.OrdinalIgnoreCase);

        }

        public static bool IsAccessDeniedError(this System.Exception? ex)
        {
            if (ex == null) return false;

            return ex.Message.Contains(Constants.EdrsAccessDeniedMessage, StringComparison.OrdinalIgnoreCase) ||
                  ex.Message.Contains(Constants.EDRSHTTPREQUESTFORBIDDEN, StringComparison.OrdinalIgnoreCase);

        }

        public static bool IsSchemaError(this System.Exception? ex)
        {
            if (ex == null || ex.GetType() != typeof(System.ServiceModel.FaultException<string>)) return false;

            return string.Equals(ex.Message, "Schema Errors have occured");
        }

        public static string? GetSchemaErrorText(this System.Exception ex)
        {
            var exception = ex as System.ServiceModel.FaultException<string>;
            return exception == null ? null : $"{exception.Message} >> {exception.Detail}";
        }

        internal static async Task<JToken?> GetLogJToken(string logPrefix, List<string> fields, string? json)
        {
            return await Task.Run(() =>
            {
                JObject? jObject = null;
                if (json != null)
                {
                    try
                    {
                        jObject = JObject.Parse(json);
                        foreach (var field in fields)
                        {
                            var fieldValue = jObject.SelectToken(field);
                            if (fieldValue != null)
                            {
                                jObject.SelectToken(field)?.Replace("XXXXX");
                            }
                        }
                    }
                    catch (System.Exception)
                    {
                        // Optionally, handle the exception if needed.
                    }
                }

                return jObject;
            });
        }

        public static XElement ConvertObjectToXmlElement(object obj)
        {
            ArgumentNullException.ThrowIfNull(obj); // No need to specify 'nameof(obj)'

            try
            {
                var serializer = new XmlSerializer(obj.GetType());

                // Use a MemoryStream to handle XML serialization
                using (var memoryStream = new MemoryStream())
                using (var xmlWriter = XmlWriter.Create(memoryStream, new XmlWriterSettings
                {
                    Indent = true,            // Beautifies the XML with indents
                    OmitXmlDeclaration = true // Omits the XML declaration (optional)
                }))
                {
                    // Serialize the object to XML
                    serializer.Serialize(xmlWriter, obj);
                    memoryStream.Position = 0; // Reset the stream position to read

                    // Load the resulting XML into an XElement
                    return XElement.Load(memoryStream);
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Error converting object to XML.", ex);
            }
        }


    }
}
