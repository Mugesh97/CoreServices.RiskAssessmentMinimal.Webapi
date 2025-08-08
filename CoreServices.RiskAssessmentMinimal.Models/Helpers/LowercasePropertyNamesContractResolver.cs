using Newtonsoft.Json.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Edrs.ActionListenerService.Models.Helpers
{
    [ExcludeFromCodeCoverage]
    public class LowercasePropertyNamesContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return propertyName;

            return char.ToLower(propertyName[0], CultureInfo.InvariantCulture) + propertyName.Substring(1);
        }

    }
}
