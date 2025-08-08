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
    public class ApplicationConverter : EDRSSubmitConverter<ApplicationBase>
    {
        protected override EDRSSubmitConverter<ApplicationBase> GetOwnInstance()
        {
            return new ApplicationConverter();
        }

        protected override object GetPopulatedObject(JObject jsonObject, JsonSerializer serializer, Type objectType, object existingValue)
        {
            string appType = string.Empty;
            //Get exception type
            var invalidDtoException = new InvalidEDRSSubmitParametersException();

            //Validate the applicationType if it is passed in
            if (jsonObject.GetValue("applicationType", StringComparison.OrdinalIgnoreCase) != null)
            {
                var applicationTypeJsonProperty = jsonObject.GetValue(Application.JsonNameOfApplicationTypeProperty, StringComparison.OrdinalIgnoreCase);
                {
                    if (applicationTypeJsonProperty != null)
                    {
                        try
                        {
                            appType = applicationTypeJsonProperty.ToString();
                            if (!EnumHelper.IsDefinedCaseInsensitive(typeof(AppType), applicationTypeJsonProperty.ToString()))
                            {
                                throw invalidDtoException;
                            }
                        }
                        catch (Exception ex)
                        {
                            var identifierProperty = applicationTypeJsonProperty.Path;

                            var constraint = "must be of type ApplicationType";

                            invalidDtoException.DtoValidationMessage = $" - {identifierProperty} is not valid with respect to validation constraint: {constraint}";
                            throw invalidDtoException;
                        }
                    }
                }
            }
            if (jsonObject.GetValue("copyType", StringComparison.OrdinalIgnoreCase) != null)
            {
                var applicationCopyTypeJsonProperty = jsonObject.GetValue(Application.JsonNameOfCopyTypeProperty, StringComparison.OrdinalIgnoreCase);
                if (applicationCopyTypeJsonProperty != null)
                {
                    try
                    {
                        if (
                            (appType != AppType.DIS.ToString() && appType != AppType.COA.ToString()) ||
                            ((appType == AppType.DIS.ToString() || appType == AppType.COA.ToString())
                            && applicationCopyTypeJsonProperty.ToString() != "0")
                           )
                        {
                            if (!EnumHelper.IsDefinedCaseInsensitive(typeof(DocumentCopyType), applicationCopyTypeJsonProperty.ToString()))
                            {
                                throw invalidDtoException;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        var identifierProperty = applicationCopyTypeJsonProperty.Path;

                        var constraint = "must be of type DocumentCopyType";

                        invalidDtoException.DtoValidationMessage = $" - {identifierProperty} is not valid with respect to validation constraint: {constraint}";
                        throw invalidDtoException;
                    }
                }
            }
            var application = default(ApplicationBase);

            var generalTypeJsonProperty = jsonObject.GetValue(ApplicationBase.JsonNameOfTypeProperty, StringComparison.OrdinalIgnoreCase);
            {
                if (generalTypeJsonProperty != null)
                {
                    var generalType = new ApplicationBase.Type();
                    try
                    {
                        if (!EnumHelper.IsDefinedCaseInsensitive(typeof(ApplicationBase.Type), generalTypeJsonProperty.ToString()))
                        {
                            throw invalidDtoException;
                        }
                        generalType = As<ApplicationBase.Type>(generalTypeJsonProperty);
                    }
                    catch (Exception ex)
                    {
                        var identifierProperty = generalTypeJsonProperty.Path;

                        var constraint = "Other or Charge";

                        invalidDtoException.DtoValidationMessage = $" - {identifierProperty} is not valid with respect to validation constraint: {constraint}";
                        throw invalidDtoException;
                    }

                    switch (As<ApplicationBase.Type>(generalTypeJsonProperty))
                    {
                        case ApplicationBase.Type.Charge:
                            application = new ChargeApplication();
                            break;
                        case ApplicationBase.Type.Other:
                            application = new Application();
                            break;
                    }
                }
            }
            serializer.Populate(jsonObject.CreateReader(), application);

            return application;
        }
    }
}
