//using Landmark.HmlrBg.Core.Services;

using Edrs.ActionListenerService.Models.ModelValidationRules;
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.ApplicationDoc;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(IdentityFormValidator))]
    public class IdentityForm : Document
    {
        public const string JsonNameOfTypeProperty = "copytype";
    }
}
