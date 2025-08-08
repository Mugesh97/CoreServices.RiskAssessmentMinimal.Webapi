using FluentValidation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Edrs.ActionListenerService.Models.ModelValidationRules
{
    [ExcludeFromCodeCoverage]
    internal static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, ICollection<string>> UniqueValues<T>(this IRuleBuilder<T, ICollection<string>> ruleBuilder)
        {
            return ruleBuilder.Must(collection => collection.Count == collection.Distinct().ToList().Count);
        }

        public static IRuleBuilderOptions<T, string> IsInValuesOf<T>(this IRuleBuilder<T, string> ruleBuilder, Type validTypesClass)
        {
            return ruleBuilder.Must(stringValue => IsInValuesOf(stringValue, validTypesClass));
        }

        public static IRuleBuilderOptions<T, string> IsValidFileName<T>(this IRuleBuilder<T, string> ruleBuilder, bool isEmptyValid)
        {
            return ruleBuilder.Must(fileName => IsValidFileName(fileName, isEmptyValid));
        }

        #region Private Helpers

        private static bool IsInValuesOf(string stringValue, Type validTypesClass)
        {
            if (string.IsNullOrEmpty(stringValue))
                return false;

            List<string> validValues = validTypesClass.GetFields(BindingFlags.Static | BindingFlags.Public)
                .Select(x => x.GetValue(null)).Cast<string>().ToList();

            return validValues.Any(t => t == stringValue);
        }

        private static bool IsValidFileName(string fileName, bool isEmptyValid)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return isEmptyValid;
            }

            var endsWithFileExtensionFormat = Regex.IsMatch(fileName, @"\.[A-Za-z0-9]+$");
            if (!endsWithFileExtensionFormat)
            {
                return false;
            }

            bool isValid = fileName.IndexOfAny(Path.GetInvalidFileNameChars()) < 0;
            return isValid;
        }

        #endregion
    }
}
