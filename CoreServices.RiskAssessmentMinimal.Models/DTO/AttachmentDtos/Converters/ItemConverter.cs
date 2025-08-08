using Edrs.ActionListenerService.Models.DTO.AttachmentDtos.Types;
using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Converters;
using Edrs.ActionListenerService.Models.Exceptions;
using Edrs.ActionListenerService.Models.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.DTO.AttachmentDtos.Converters
{
    [ExcludeFromCodeCoverage]
    public class ItemConverter : EDRSSubmitConverter<Item>
    {
        protected override EDRSSubmitConverter<Item> GetOwnInstance()
        {
            return new ItemConverter();
        }

        protected override object GetPopulatedObject(JObject jsonObject, JsonSerializer serializer, Type objectType, object existingValue)
        {
            //Get exception type
            var invalidDtoException = new InvalidEDRSAttachmentParametersException();

            var item = default(Item);

            var jsonProperty = jsonObject.GetValue(Item.JsonNameOfTypeProperty, StringComparison.OrdinalIgnoreCase);

            if (jsonProperty != null)
            {
                try
                {
                    //Add validate item method here, pre-assignment - to catch case sensitivity
                    if (!EnumHelper.IsDefinedCaseInsensitive(typeof(Item.Type), jsonProperty.ToString()))
                    {
                        throw invalidDtoException;
                    }
                }
                catch (Exception ex)
                {
                    var itemProperty = jsonProperty.Path;

                    var constraint = "Attachment or Note";

                    invalidDtoException.DtoValidationMessage = $" - {itemProperty} is not valid with respect to validation constraint: {constraint}";
                    throw invalidDtoException;
                }

                switch (As<Item.Type>(jsonProperty))
                {
                    case Item.Type.Attachment:
                        item = new Attachment();
                        item.ValidateValue(jsonObject.GetValue("copyType", StringComparison.OrdinalIgnoreCase).ToString());
                        break;
                    case Item.Type.Note:
                        item = new Note();
                        break;
                }

                serializer.Populate(jsonObject.CreateReader(), item);
            }
            return item;
        }
    }
}
