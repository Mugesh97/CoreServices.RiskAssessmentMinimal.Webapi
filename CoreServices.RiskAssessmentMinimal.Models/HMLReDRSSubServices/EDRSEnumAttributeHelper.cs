using System;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.HMLReDRSSubServices
{
    [ExcludeFromCodeCoverage]
    public static class EDRSEnumAttributeHelper
    {
        public static T ToEDRSEnum<T>(this Enum enumVal)
        {
            var field = enumVal.GetType().GetField(enumVal.ToString());
            var attributes = AttributeHelper.GetAllCustomAttributes(field);
            if (attributes.Length > 0)
            {
                Edrs.ActionListenerService.Models.Helpers.EDRSEnumAttribute val = (Edrs.ActionListenerService.Models.Helpers.EDRSEnumAttribute)attributes[0];
                T value = (T)Enum.Parse(typeof(T), val.EnumValue.ToString());
                return value;
            }
            else
            {
                throw new Exception($"EDRSEnumAttribute was not found on the passed enum {enumVal}");
            }
        }
    }
}
