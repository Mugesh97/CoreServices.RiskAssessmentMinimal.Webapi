using System;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules
{
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    internal class ValidatorAttribute : Attribute
    {
        public Type ValidatorType { get; set; }

        public ValidatorAttribute(Type validatorType)
        {
            ValidatorType = validatorType;
        }
    }
}
