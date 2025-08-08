using System;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.Helpers
{
    public static class Extentions
    {
        [ExcludeFromCodeCoverage]
        public static int GetIntegerValue(this string value)
        {
            return Convert.ToInt16(value);
        }
    }
}
