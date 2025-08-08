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
    public class SupportingDocumentConverter : EDRSSubmitConverter<SupportingDocument>
    {
        protected override EDRSSubmitConverter<SupportingDocument> GetOwnInstance()
        {
            return new SupportingDocumentConverter();
        }

        protected override object GetPopulatedObject(JObject jsonObject, JsonSerializer serializer, Type objectType, object existingValue)
        {
            //Get exception type
            var invalidDtoException = new InvalidEDRSSubmitParametersException();

            var supportingDocument = default(SupportingDocument);

            var nameJsonProperty = jsonObject.GetValue(SupportingDocument.JsonNameOfDocumentTypeProperty, StringComparison.OrdinalIgnoreCase);
            {
                if (nameJsonProperty != null)
                {
                    var documentName = new DocumentNameType();
                    //Set DocumentName
                    try
                    {
                        if (!EnumHelper.IsDefinedCaseInsensitive(typeof(DocumentNameType), nameJsonProperty.ToString()))
                        {
                            throw invalidDtoException;
                        }
                        documentName = As<DocumentNameType>(nameJsonProperty);
                    }
                    catch (Exception ex)
                    {
                        var identifierProperty = nameJsonProperty.Path;

                        var constraint = "must be of type DocumentNameType";

                        invalidDtoException.DtoValidationMessage = $" - {identifierProperty} is not valid with respect to validation constraint: {constraint}";
                        throw invalidDtoException;
                    }
                }
            }
            var copyJsonProperty = jsonObject.GetValue(SupportingDocument.JsonNameOfCopyTypeProperty, StringComparison.OrdinalIgnoreCase);
            {
                if (copyJsonProperty != null)
                {
                    //Set DocumentCopyType
                    try
                    {
                        if (!EnumHelper.IsDefinedCaseInsensitive(typeof(DocumentCopyType), copyJsonProperty.ToString()))
                        {
                            throw invalidDtoException;
                        }
                    }
                    catch (Exception ex)
                    {
                        var identifierProperty = copyJsonProperty.Path;

                        var constraint = "must be of type DocumentCopyType";

                        invalidDtoException.DtoValidationMessage = $" - {identifierProperty} is not valid with respect to validation constraint: {constraint}";
                        throw invalidDtoException;
                    }
                }
            }
            supportingDocument = new SupportingDocument();

            serializer.Populate(jsonObject.CreateReader(), supportingDocument);

            return supportingDocument;
        }
    }
}
