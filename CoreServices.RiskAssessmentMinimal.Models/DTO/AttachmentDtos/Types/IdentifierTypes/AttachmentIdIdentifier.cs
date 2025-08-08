using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Edrs.ActionListenerService.Models.Exceptions;
using Edrs.ActionListenerService.Models.ModelValidationRules;

namespace Edrs.ActionListenerService.Models.DTO.AttachmentDtos.Types.IdentifierTypes
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(AttachmentIdIdentifier))]
    public class AttachmentIdIdentifier : Identifier
    {
        [NumberMinMaxValidation(1)]
        [DataMember(IsRequired = true)]
        public int? Value { get; set; }

        public AttachmentIdIdentifier() : base(Type.AttachmentId)
        {

        }

        public override void ValidateValue(string valueToValidate)
        {
            var invalidDtoException = new InvalidEDRSAttachmentParametersException();

            var integerToValidate = 0;

            var isInt = Int32.TryParse(valueToValidate, out integerToValidate);

            if (string.IsNullOrWhiteSpace(valueToValidate))
            {
                var constraint = "cannot be null";

                invalidDtoException.DtoValidationMessage = $" - AttachmentId value is not valid with respect to validation constraint: {constraint}";
                throw invalidDtoException;
            }
            else if (isInt != true)
            {
                var constraint = "has to be a positive integer";

                invalidDtoException.DtoValidationMessage = $" - AttachmentId value is not valid with respect to validation constraint: {constraint}";
                throw invalidDtoException;
            }
            else if (isInt = true & integerToValidate < 1)
            {
                var constraint = "has to be a positive integer";

                invalidDtoException.DtoValidationMessage = $" - AttachmentId value is not valid with respect to validation constraint: {constraint}";
                throw invalidDtoException;
            }
        }
    }
}
