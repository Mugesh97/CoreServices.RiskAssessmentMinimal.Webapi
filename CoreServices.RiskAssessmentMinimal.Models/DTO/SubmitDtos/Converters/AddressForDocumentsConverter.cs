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
    public class AddressForDocumentsConverter : EDRSSubmitConverter<AddressForDocuments>
    {
        protected override EDRSSubmitConverter<AddressForDocuments> GetOwnInstance()
        {
            return new AddressForDocumentsConverter();
        }

        protected override object GetPopulatedObject(JObject jsonObject, JsonSerializer serializer, Type objectType, object existingValue)
        {
            //Get exception type
            var invalidDtoException = new InvalidEDRSSubmitParametersException();

            var address = default(AddressForDocuments);

            var jsonProperty = jsonObject.GetValue(AddressForDocuments.JsonNameOfTypeProperty, StringComparison.OrdinalIgnoreCase);

            if (jsonProperty != null)
            {
                try
                {
                    if (!EnumHelper.IsDefinedCaseInsensitive(typeof(AddressForDocuments.Type), jsonProperty.ToString()))
                    {
                        throw invalidDtoException;
                    }
                }
                catch (Exception ex)
                {
                    var identifierProperty = jsonProperty.Path;

                    var constraint = "must be SubjectPropertyAddress, SellersAddress, TransferOrAssentAddress or SpecificAddress";

                    invalidDtoException.DtoValidationMessage = $" - {identifierProperty} is not valid with respect to validation constraint: {constraint}";
                    throw invalidDtoException;
                }

                switch (As<AddressForDocuments.Type>(jsonProperty))
                {
                    case AddressForDocuments.Type.SpecificAddress:
                        address = new SpecificAddress();
                        break;
                    case AddressForDocuments.Type.SubjectPropertyAddress:
                        address = new SubjectPropertyAddress();
                        break;
                    case AddressForDocuments.Type.SellersAddress:
                        address = new SellersAddress();
                        break;
                    case AddressForDocuments.Type.TransferOrAssentAddress:
                        address = new TransferOrAssentAddress();
                        break;
                }
                serializer.Populate(jsonObject.CreateReader(), address);
            }
            return address;
        }
    }
}
