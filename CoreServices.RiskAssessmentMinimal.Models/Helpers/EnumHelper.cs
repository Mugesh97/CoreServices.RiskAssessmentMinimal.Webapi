
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Edrs.ActionListenerService.Models.Helpers
{
    public static class EnumHelper
    {
        [ExcludeFromCodeCoverage]
        public static bool IsDefinedCaseInsensitive(Type enumType, string value)
        {
            var isDefined = Enum.GetNames(enumType).Any(x => x.ToLower() == value.ToLower());
            if (isDefined)
            {
                return true;
            }
            return false;
        }
    }
}