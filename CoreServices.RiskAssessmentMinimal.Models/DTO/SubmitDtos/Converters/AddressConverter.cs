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
    public class AddressConverter : EDRSSubmitConverter<Address>
    {
        protected override EDRSSubmitConverter<Address> GetOwnInstance()
        {
            return new AddressConverter();
        }

        protected override object GetPopulatedObject(JObject jsonObject, JsonSerializer serializer, Type objectType, object existingValue)
        {
            //Get exception type
            var invalidDtoException = new InvalidEDRSSubmitParametersException();
            var address = default(Address);

            var jsonProperty = jsonObject.GetValue(Address.JsonNameOfTypeProperty, StringComparison.OrdinalIgnoreCase);

            if (jsonProperty != null)
            {
                try
                {
                    if (!EnumHelper.IsDefinedCaseInsensitive(typeof(Address.Type), jsonProperty.ToString()))
                    {
                        throw invalidDtoException;
                    }
                }
                catch (Exception ex)
                {
                    var identifierProperty = jsonProperty.Path;

                    var constraint = "Email or Mail";

                    invalidDtoException.DtoValidationMessage = $" - {identifierProperty} is not valid with respect to validation constraint: {constraint}";
                    throw invalidDtoException;
                }

                switch (As<Address.Type>(jsonProperty))
                {
                    case Address.Type.Email:
                        address = new EmailAddress();
                        break;
                    case Address.Type.Mail:
                        return new MailAddressConverter().ReadJson(jsonObject.CreateReader(), objectType, existingValue, serializer);
                }

                serializer.Populate(jsonObject.CreateReader(), address);
            }
            return address;
        }
    }
}
