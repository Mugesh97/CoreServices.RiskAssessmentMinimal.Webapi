using Edrs.ActionListenerService.Models.ModelValidationRules.Validators;
using FluentValidation;


namespace Edrs.ActionListenerService.Models.ModelValidationRules
{
    public interface IValidatorProviderHelp
    {
        IValidator<T> GetExactTypeValidator<T>();

        PolymorphicValidator<T> GetPolymorphicValidators<T>() where T : class;

        string GetCantBeBlankTextValueMsg(string propertyName);
        string GetAppearsToHaveInvalidValueMsg(string propertyName);
        string GetMustBeSetMsg(string propertyName);
        string GetMustBeGreaterThanOrEqualToZeroMsg(string propertyName);
        string GetMustBeGreaterThanOneMsg(string propertyName);
        string GetMustHaveUniqueAttachmentIdMsg(string propertyName);
    }
}
