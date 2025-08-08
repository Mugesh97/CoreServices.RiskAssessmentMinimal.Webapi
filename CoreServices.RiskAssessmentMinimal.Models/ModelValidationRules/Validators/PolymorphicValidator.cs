using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.ModelValidationRules.Validators
{
    [ExcludeFromCodeCoverage]
    public class PolymorphicValidator<TBaseClass> : AbstractValidator<TBaseClass>
           where TBaseClass : class
    {
        private readonly Dictionary<Type, IValidator> _derivedValidators = new Dictionary<Type, IValidator>();

        public PolymorphicValidator<TBaseClass> Add<TDerived>(IValidator<TDerived> derivedValidator) where TDerived : TBaseClass
        {
            _derivedValidators[typeof(TDerived)] = derivedValidator;
            return this;
        }

        public override ValidationResult Validate(ValidationContext<TBaseClass> context)
        {
            // bail out if the property is null 
            if (context.InstanceToValidate == null)
                return new ValidationResult();

            return base.Validate(context);
        }    
      
    }
}
