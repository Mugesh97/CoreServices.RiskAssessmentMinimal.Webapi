using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Edrs.ActionListenerService.Models.Exceptions;
using Edrs.ActionListenerService.Models.ModelValidationRules;
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.AttachmentValidations.IdentifierTypes;
using static Edrs.ActionListenerService.Models.Common.Enums;

namespace Edrs.ActionListenerService.Models.DTO.AttachmentDtos.Types.IdentifierTypes
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(DocumentNameIdentifierValidator))]
    public class DocumentNameIdentifier : Identifier
    {
        [ValueValidation()]
        [DataMember(IsRequired = true)]
        public DocumentNameType Value { get; set; }

        public DocumentNameIdentifier() : base(Type.DocumentName)
        {

        }
        public override void ValidateValue(string valueToValidate)
        {
            var invalidDtoException = new InvalidEDRSAttachmentParametersException();

            if (string.IsNullOrWhiteSpace(valueToValidate.ToString()))
            {
                var constraint = "cannot be null";

                invalidDtoException.DtoValidationMessage = $" - DocumentName value is not valid with respect to validation constraint: {constraint}";
                throw invalidDtoException;
            }
            else if (!Enum.IsDefined(typeof(DocumentNameType), valueToValidate))
            {
                var constraint = "must be of type DocumentNameContent";

                invalidDtoException.DtoValidationMessage = $" - DocumentName value is not valid with respect to validation constraint: {constraint}";
                throw invalidDtoException;
            }
        }
    }
}
