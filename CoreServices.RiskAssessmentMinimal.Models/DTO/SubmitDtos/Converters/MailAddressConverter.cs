using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using Edrs.ActionListenerService.Models.Exceptions;
using Edrs.ActionListenerService.Models.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Converters
{
    [ExcludeFromCodeCoverage]
    public class MailAddressConverter : EDRSSubmitConverter<MailAddress>
    {
        protected override EDRSSubmitConverter<MailAddress> GetOwnInstance()
        {
            return new MailAddressConverter();
        }

        protected override object GetPopulatedObject(JObject jsonObject, JsonSerializer serializer, Type objectType, object existingValue)
        {
            //Get exception type
            var invalidDtoException = new InvalidEDRSSubmitParametersException();

            var address = default(MailAddress);

            var jsonProperty = jsonObject.GetValue(MailAddress.JsonNameOfTypeProperty, StringComparison.OrdinalIgnoreCase);
            {
                if (jsonProperty != null)
                {
                    try
                    {
                        if (!EnumHelper.IsDefinedCaseInsensitive(typeof(MailAddress.Type), jsonProperty.ToString()))
                        {
                            throw invalidDtoException;
                        }
                    }
                    catch (Exception ex)
                    {
                        var identifierProperty = jsonProperty.Path;

                        var constraint = "must be Postal or DX";

                        invalidDtoException.DtoValidationMessage = $" - {identifierProperty} is not valid with respect to validation constraint: {constraint}";
                        throw invalidDtoException;
                    }

                    switch (As<MailAddress.Type>(jsonProperty))
                    {
                        case MailAddress.Type.Postal:
                            address = new PostalAddress();
                            break;
                        case MailAddress.Type.DX:
                            address = new DXAddress();
                            break;
                    }
                    serializer.Populate(jsonObject.CreateReader(), address);
                }
            }
            return address;
        }
    }
}
