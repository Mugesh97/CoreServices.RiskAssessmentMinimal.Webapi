using Edrs.ActionListenerService.Models.Exceptions;
using System.Runtime.Serialization;
using Edrs.ActionListenerService.Models.ModelValidationRules;
using static Edrs.ActionListenerService.Models.Common.Enums;
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.AttachmentValidations.IdentifierTypes;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.DTO.AttachmentDtos.Types.IdentifierTypes
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(ApplicationIdentifierValidator))]
    public class ApplicationIdentifier : Identifier
    {
        [ValueValidation]
        [DataMember(IsRequired = true)]
        public AppType Value { get; set; }

        public ApplicationIdentifier() : base(Type.Application)
        {

        }

        public override void ValidateValue(string valueToValidate)
        {
            var invalidDtoException = new InvalidEDRSAttachmentParametersException();

            if (string.IsNullOrWhiteSpace(valueToValidate))
            {
                var constraint = "cannot be null";

                invalidDtoException.DtoValidationMessage = $" - Application value is not valid with respect to validation constraint: {constraint}";
                throw invalidDtoException;
            }
            else if (!Enum.IsDefined(typeof(AppType), valueToValidate))
            {
                var constraint = "must be of type ApplicationTypeContent";

                invalidDtoException.DtoValidationMessage = $" - Application value is not valid with respect to validation constraint: {constraint}";
                throw invalidDtoException;
            }
        }
    }
}
