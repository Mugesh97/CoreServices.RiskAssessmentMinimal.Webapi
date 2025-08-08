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
    public class PartyConverter : EDRSSubmitConverter<Party>
    {
        protected override EDRSSubmitConverter<Party> GetOwnInstance()
        {
            return new PartyConverter();
        }

        protected override object GetPopulatedObject(JObject jsonObject, JsonSerializer serializer, Type objectType, object existingValue)
        {
            //Get exception type
            var invalidDtoException = new InvalidEDRSSubmitParametersException();
            var party = default(Party);

            var jsonProperty = jsonObject.GetValue(Party.JsonNameOfTypeProperty, StringComparison.OrdinalIgnoreCase);
            {
                try
                {
                    if (!EnumHelper.IsDefinedCaseInsensitive(typeof(Party.Type), jsonProperty.ToString()))
                    {
                        throw invalidDtoException;
                    }
                }
                catch (Exception ex)
                {
                    var identifierProperty = string.Empty;
                    try
                    {
                        identifierProperty = jsonProperty.Path;
                    }
                    //if the property doesn't exist
                    catch (NullReferenceException nullEx)
                    {
                        invalidDtoException.DtoValidationMessage = $" - party details must be present for each conveyancer";
                        throw invalidDtoException;
                    }
                    var constraint = "must be Person or Company";

                    invalidDtoException.DtoValidationMessage = $" - {identifierProperty} is not valid with respect to validation constraint: {constraint}";
                    throw invalidDtoException;
                }

                switch (As<Party.Type>(jsonProperty))
                {
                    case Party.Type.Person:
                        party = new Person();
                        party.ValidateRole(jsonObject);
                        break;
                    case Party.Type.Company:
                        party = new Company();
                        party.ValidateRole(jsonObject);
                        party.ValidateParty(jsonObject);
                        break;
                }
                serializer.Populate(jsonObject.CreateReader(), party);
            }
            return party;
        }
    }
}
