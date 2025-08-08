using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Edrs.ActionListenerService.Models.Helpers
{
    [ExcludeFromCodeCoverage]
    public class StringHelper
    {
        public static string LowercaseFirstLetter(string input)
        {
            return input.Substring(0, 1).ToLower() + input.Substring(1);
        }
        public static bool ValidateStringIsRestrictedCharacters(string value, string regex)
        {
            //Regex regex = new Regex(@"^[a-zA-Z0-9_()\,\'\.\-\s]+$");
            Regex ex = new Regex(regex);
            Match match = ex.Match(value);
            if (match.Success)
            {
                return true;
            }

            return false;
        }

    }
}