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
    public class TitleNumbersConverter : EDRSSubmitConverter<TitleNumbers>
    {
        protected override EDRSSubmitConverter<TitleNumbers> GetOwnInstance()
        {
            return new TitleNumbersConverter();
        }

        protected override object GetPopulatedObject(JObject jsonObject, JsonSerializer serializer, Type objectType, object existingValue)
        {
            //Get exception type
            var invalidDtoException = new InvalidEDRSSubmitParametersException();

            var titleNumbers = default(TitleNumbers);

            var titleNumberJsonProperty = jsonObject.GetValue(TitleNumbers.JsonNameOfTypeProperty, StringComparison.OrdinalIgnoreCase);
            {
                if (titleNumberJsonProperty != null)
                {
                    try
                    {
                        if (!EnumHelper.IsDefinedCaseInsensitive(typeof(TitleNumbers.Type), titleNumberJsonProperty.ToString()))
                        {
                            throw invalidDtoException;
                        }
                    }
                    catch (Exception ex)
                    {
                        var identifierProperty = titleNumberJsonProperty.Path;

                        var constraint = "Dealing, TransferOfPart, DocumentName, NewLease or LeaseExtension";

                        invalidDtoException.DtoValidationMessage = $" - {identifierProperty} is not valid with respect to validation constraint: {constraint}";
                        throw invalidDtoException;
                    }

                    switch (As<TitleNumbers.Type>(titleNumberJsonProperty))
                    {
                        case TitleNumbers.Type.Dealing:
                            titleNumbers = new DealingTitleNumbers();
                            break;
                        case TitleNumbers.Type.LeaseExtension:
                            titleNumbers = new LeaseExtensionTitleNumbers();
                            break;
                        case TitleNumbers.Type.NewLease:
                            titleNumbers = new NewLeaseTitleNumbers();
                            break;
                        case TitleNumbers.Type.TransferOfPart:
                            titleNumbers = new TransferOfPartTitleNumbers();
                            break;
                    }
                    serializer.Populate(jsonObject.CreateReader(), titleNumbers);
                }
            }
            return titleNumbers;
        }
    }
}
