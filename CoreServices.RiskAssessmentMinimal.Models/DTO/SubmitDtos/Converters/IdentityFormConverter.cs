using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using Edrs.ActionListenerService.Models.Exceptions;
using Edrs.ActionListenerService.Models.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics.CodeAnalysis;
using static Edrs.ActionListenerService.Models.Common.Enums;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Converters
{
    [ExcludeFromCodeCoverage]
    public class IdentityFormConverter : EDRSSubmitConverter<IdentityForm>
    {
        protected override EDRSSubmitConverter<IdentityForm> GetOwnInstance()
        {
            return new IdentityFormConverter();
        }

        protected override object GetPopulatedObject(JObject jsonObject, JsonSerializer serializer, Type objectType, object existingValue)
        {
            //Get exception type
            var invalidDtoException = new InvalidEDRSSubmitParametersException();

            var identityForm = default(IdentityForm);

            var jsonProperty = jsonObject.GetValue(IdentityForm.JsonNameOfTypeProperty, StringComparison.OrdinalIgnoreCase);

            if (jsonProperty != null)
            {
                try
                {
                    if (!EnumHelper.IsDefinedCaseInsensitive(typeof(DocumentCopyType), jsonProperty.ToString()))
                    {
                        throw invalidDtoException;
                    }
                }
                catch (Exception ex)
                {
                    var identifierProperty = jsonProperty.Path;

                    invalidDtoException.DtoValidationMessage = $" - {identifierProperty} is not valid with respect to validation constraint: must be of type DocumentCopyType.";
                    throw invalidDtoException;
                }

                identityForm = new IdentityForm();

                serializer.Populate(jsonObject.CreateReader(), identityForm);
            }
            return identityForm;
        }
    }
}
