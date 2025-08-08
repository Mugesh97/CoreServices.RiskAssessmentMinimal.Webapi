using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Edrs.ActionListenerService.Models.HMLReDRSSubServices
{
    [ExcludeFromCodeCoverage]
    public class AttributeHelper
    {
        // Method to get all custom attributes of a member
        public static object[] GetAllCustomAttributes(MemberInfo memberInfo, bool inherit = true)
        {
            // Retrieves all custom attributes applied to the member
            return memberInfo.GetCustomAttributes(inherit);
        }

        // Method to get specific custom attributes of a member
        public static object[] GetSpecificCustomAttributes(MemberInfo memberInfo, Type attributeType, bool inherit = true)
        {
            // Retrieves custom attributes of a specific type applied to the member
            return memberInfo.GetCustomAttributes(attributeType, inherit);
        }
       
    }
}
