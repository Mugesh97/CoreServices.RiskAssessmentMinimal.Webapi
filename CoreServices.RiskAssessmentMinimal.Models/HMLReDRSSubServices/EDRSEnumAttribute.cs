using System;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.HMLReDRSSubServices
{
    [ExcludeFromCodeCoverage]
    public class EDRSEnumAttribute : Attribute
    {
        public object EnumValue { get; set; }

        public EDRSEnumAttribute(object enumValue)
        {
            EnumValue = enumValue;
        }
    }
}
