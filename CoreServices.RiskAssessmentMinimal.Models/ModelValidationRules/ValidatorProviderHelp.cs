using Edrs.ActionListenerService.Models.ModelValidationRules.Validators;
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.AttachmentValidations;
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.AttachmentValidations.IdentifierTypes;
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.Address;
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.ApplicationDoc;
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.Party;
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.ServiceAddress;
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.TitleNumber;
using FluentValidation;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Edrs.ActionListenerService.Models.ModelValidationRules
{
    [ExcludeFromCodeCoverage]
    public class ValidatorProviderHelp : IValidatorProviderHelp
    {
        public IValidator<T> GetExactTypeValidator<T>()
        {
            var attribute = typeof(T).GetCustomAttributes(typeof(ValidatorAttribute), false).FirstOrDefault() as ValidatorAttribute;
            if (attribute == null)
                throw new ArgumentException($"{nameof(ValidatorAttribute)} not found on the property {typeof(T).FullName}");

            var validator = (IValidator<T>)Activator.CreateInstance(attribute.ValidatorType);
            return validator;
        }

        public PolymorphicValidator<T> GetPolymorphicValidators<T>() where T : class
        {
            object validators = null;
            if (typeof(T) == typeof(DTO.SubmitDtos.Types.Address))
            {
                validators = new PolymorphicValidator<DTO.SubmitDtos.Types.Address>()
                    .Add(new DXMailAddressValidator())
                .Add(new PostalMailAddressValidator())
                    .Add(new EmailAddressValidator());
            }
            else if (typeof(T) == typeof(DTO.SubmitDtos.Types.MailAddress))
            {
                validators = new PolymorphicValidator<DTO.SubmitDtos.Types.MailAddress>()
                .Add(new DXMailAddressValidator())
                    .Add(new PostalMailAddressValidator());
            }
            else if (typeof(T) == typeof(DTO.SubmitDtos.Types.Party))
            {
                validators = new PolymorphicValidator<DTO.SubmitDtos.Types.Party>()
                .Add(new CompanyPartyValidator())
                    .Add(new PersonPartyValidator());
            }
            else if (typeof(T) == typeof(DTO.SubmitDtos.Types.AddressForDocuments))
            {
                validators = new PolymorphicValidator<DTO.SubmitDtos.Types.AddressForDocuments>()
                    .Add(new SellersAddressForDocsValidator())
                .Add(new SpecificAddressForDocsValidator())
                    .Add(new SubjectPropertyAddressForDocsValidator())
                    .Add(new TransferOrAssentAddressForDocsValidator());
            }
            else if (typeof(T) == typeof(DTO.SubmitDtos.Types.TitleNumbers))
            {
                validators = new PolymorphicValidator<DTO.SubmitDtos.Types.TitleNumbers>()
                    .Add(new DealingTitleNumbersValidator())
                .Add(new LeaseExtensionTitleNumbersValidator())
                    .Add(new NewLeaseTitleNumbersValidator())
                    .Add(new TransferOfPartTitleNumbersValidator());
            }
            else if (typeof(T) == typeof(DTO.SubmitDtos.Types.ApplicationBase))
            {
                validators = new PolymorphicValidator<DTO.SubmitDtos.Types.ApplicationBase>()
                    .Add(new OtherApplicationValidator())
                    .Add(new ChargeApplicationValidator());
            }
            else if (typeof(T) == typeof(DTO.AttachmentDtos.Types.Item))
            {
                validators = new PolymorphicValidator<DTO.AttachmentDtos.Types.Item>()
                    .Add(new NoteValidator())
                    .Add(new AttachmentValidator());
            }
            else if (typeof(T) == typeof(DTO.AttachmentDtos.Types.Identifier))
            {
                validators = new PolymorphicValidator<DTO.AttachmentDtos.Types.Identifier>()
                    .Add(new ApplicationIdentifierValidator())
                    .Add(new AttachmentIdIdentifierValidator())
                    .Add(new DocumentNameIdentifierValidator());
            }

            if (validators == null)
            {
                throw new ArgumentException($"Type {typeof(T).Name} is not supported");
            }

            return (PolymorphicValidator<T>)Convert.ChangeType(validators, typeof(PolymorphicValidator<T>));
        }

        public string GetCantBeBlankTextValueMsg(string propertyName)
        {
            return $"{propertyName} can't be a blank text value";
        }

        public string GetAppearsToHaveInvalidValueMsg(string propertyName)
        {
            return $"{propertyName} appears to have an invalid value";
        }

        public string GetMustBeSetMsg(string propertyName)
        {
            return $"{propertyName} must be set";
        }

        public string GetMustBeGreaterThanOrEqualToZeroMsg(string propertyName)
        {
            return $"{propertyName} must be greater than or equal to 0";
        }

        public string GetMustBeGreaterThanOneMsg(string propertyName)
        {
            return $"{propertyName} must be greater than or equal to 1";
        }

        public string GetMustHaveUniqueAttachmentIdMsg(string propertyName)
        {
            return $"{propertyName} must have the id of the associated attachment";
        }
    }
}
