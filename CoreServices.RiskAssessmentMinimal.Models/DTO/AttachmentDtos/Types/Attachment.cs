using Edrs.ActionListenerService.Models.DTO.AttachmentDtos.Converters;
using Edrs.ActionListenerService.Models.Exceptions;
using Edrs.ActionListenerService.Models.ModelValidationRules;
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.AttachmentValidations;
using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using static Edrs.ActionListenerService.Models.Common.Enums;

namespace Edrs.ActionListenerService.Models.DTO.AttachmentDtos.Types
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(AttachmentValidator))]
    public class Attachment : Item
    {
        [DataValidation]
        [JsonConverter(typeof(IdentifierConverter))]
        [DataMember(IsRequired = true)]
        public Identifier Identifier { get; set; }

        [ValueValidation]
        [DataMember(IsRequired = true)]
        public DocumentCopyType CopyType { get; set; }

        [DataValidation]
        [DataMember(IsRequired = true)]
        public AttachmentContent Content { get; set; }

        public Attachment() : base(Type.Attachment)
        {

        }

        public override void ValidateValue(string valueToValidate)
        {
            var invalidDtoException = new InvalidEDRSAttachmentParametersException();

            if (string.IsNullOrWhiteSpace(valueToValidate))
            {
                var constraint = "cannot be null";

                invalidDtoException.DtoValidationMessage = $" - copyType value is not valid with respect to validation constraint: {constraint}";
                throw invalidDtoException;
            }

            if (!Enum.IsDefined(typeof(DocumentCopyType), valueToValidate))
            {
                var constraint = "must be of type DocumentCopyType";

                invalidDtoException.DtoValidationMessage = $" - copyType value is not valid with respect to validation constraint: {constraint}";
                throw invalidDtoException;
            }
        }
    }
}
