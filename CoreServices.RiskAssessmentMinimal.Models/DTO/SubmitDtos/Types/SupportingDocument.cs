using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.ApplicationDoc;
using Edrs.ActionListenerService.Models.ModelValidationRules;
using System.Runtime.Serialization;
using static Edrs.ActionListenerService.Models.Common.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(SupportingDocumentValidator))]
    public class SupportingDocument : Document
    {
        public new const string JsonNameOfDocumentTypeProperty = "documentName";
        public new const string JsonNameOfCopyTypeProperty = "copyType";

        [ValueValidation()]
        [DataMember(IsRequired = true)]
        public DocumentNameType DocumentName { get; set; }
    }
}
