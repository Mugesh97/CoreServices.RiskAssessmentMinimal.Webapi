using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.ApplicationDoc;
using Edrs.ActionListenerService.Models.ModelValidationRules;
using System.Runtime.Serialization;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(ChargeApplicationValidator))]
    public class ChargeApplication : ApplicationBase
    {
        [DataMember(IsRequired = false)]
        public DateTime ChargeDate { get; set; }

        [DataMember(IsRequired = false)]
        public string MDRef { get; set; }

        [DataMember(IsRequired = false)]
        public string LendersSortCode { get; set; }

        public ChargeApplication() : base(Type.Charge)
        {
        }
    }
}
