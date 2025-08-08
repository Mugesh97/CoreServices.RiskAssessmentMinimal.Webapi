
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.TitleNumber;
using Edrs.ActionListenerService.Models.ModelValidationRules;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(DealingTitleNumbersValidator))]
    public class DealingTitleNumbers : TitleNumbers
    {
        public DealingTitleNumbers() : base(Type.Dealing)
        { }
    }
}
