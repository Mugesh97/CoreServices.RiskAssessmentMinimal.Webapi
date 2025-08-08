using Edrs.ActionListenerService.Models.ModelValidationRules.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edrs.ActionListenerService.Models.ModelValidationRules
{
    [ExcludeFromCodeCoverage]
    public static class ValidatorProvider
    {
        private static IValidatorProviderHelp _validatorProvider;
        public const string UrlRegex = @"^http(s)?:\/\/([\w-]+.)+[\w-]+(\/[\w- .\/?%&=])?$";

        static ValidatorProvider()
        {
            _validatorProvider = new ValidatorProviderHelp();
        }

        public static void SetValidatorProviderHelp(IValidatorProviderHelp validatorProvider)
        {
            _validatorProvider = validatorProvider ?? throw new ArgumentNullException(nameof(validatorProvider));
        }

        public static IValidator<T> GetExactTypeValidator<T>()
        {
            return _validatorProvider.GetExactTypeValidator<T>();
        }

        public static PolymorphicValidator<T> GetPolymorphicValidators<T>() where T : class
        {
            return _validatorProvider.GetPolymorphicValidators<T>();
        }

        public static string GetCantBeBlankTextValueMsg(string propertyName)
        {
            return _validatorProvider.GetCantBeBlankTextValueMsg(propertyName);
        }

        public static string GetAppearsToHaveInvalidValueMsg(string propertyName)
        {
            return _validatorProvider.GetAppearsToHaveInvalidValueMsg(propertyName);
        }

        public static string GetMustBeSetMsg(string propertyName)
        {
            return _validatorProvider.GetMustBeSetMsg(propertyName);
        }

        public static string GetMustBeGreaterThanOrEqualToZeroMsg(string propertyName)
        {
            return _validatorProvider.GetMustBeGreaterThanOrEqualToZeroMsg(propertyName);
        }

        public static string GetMustBeGreaterThanOneMsg(string propertyName)
        {
            return _validatorProvider.GetMustBeGreaterThanOneMsg(propertyName);
        }

        public static string GetMustHaveUniqueAttachmentIdMsg(string propertyName)
        {
            return _validatorProvider.GetMustHaveUniqueAttachmentIdMsg(propertyName);
        }
    }
}
