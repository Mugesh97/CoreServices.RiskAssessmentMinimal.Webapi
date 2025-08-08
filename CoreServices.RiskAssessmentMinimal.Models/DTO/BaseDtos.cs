using Edrs.ActionListenerService.Models.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Edrs.ActionListenerService.Models.DTO
{
    [ExcludeFromCodeCoverage]
    public class RestrictedCharacterFieldAttribute : Attribute
    {
        public RestrictedCharacterFieldAttribute(string regex)
        {
            RestrictionRegex = regex;
        }

        public string RestrictionRegex { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class MinimumNonNullPropertyCountAttribute : Attribute
    {
        public MinimumNonNullPropertyCountAttribute(int minimumNonNullPropertyCount)
        {
            MinimumNonNullPropertyCount = minimumNonNullPropertyCount;
        }

        public int MinimumNonNullPropertyCount { get; set; }
    }

    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Property)]
    public class DataValidationAttribute : Attribute
    {
        public DataValidationAttribute()
        {
        }
    }

    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Property)]
    public class ValueValidationAttribute : Attribute
    {
        public ValueValidationAttribute()
        {
        }
    }

    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Property)]
    public class NumberMinMaxValidationAttribute : Attribute
    {
        public int? Min { get; set; }
        public int? Max { get; set; }

        public NumberMinMaxValidationAttribute(int min)
        {
            Min = min;
        }

        public NumberMinMaxValidationAttribute(int min, int max)
        {
            Min = min;
            Max = max;
        }
    }

    [ExcludeFromCodeCoverage]
    public class DtoException : Exception
    {
        public string DtoValidationMessage { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class BaseDto
    {
        [JsonIgnore]
        protected DtoException InvalidDtoException;

        public virtual DtoException GetDtoException()
        {
            return InvalidDtoException is null ? new DtoException() : InvalidDtoException;
        }

        public void Validate(string serviceName = null)
        {
            var nonNullPropertyCount = 0;
            try
            {
                foreach (var property in GetType().GetProperties())
                {
                    var objectWithValue = this;
                    //var propertyPath = $"Json: {property.Name}";
                    var propertyPath = StringHelper.LowercaseFirstLetter(property.Name);
                    ExecuteChecks(property, objectWithValue, propertyPath, serviceName);
                    nonNullPropertyCount += IncrementNonNullPropertyCountIfPropertyIsNotNull(property, this);
                }
                CheckMinimumNonNullPropertyCountIsMet(nonNullPropertyCount, this);
            }
            catch (Exception)
            {
                throw InvalidDtoException;
            }
        }

        [ExcludeFromCodeCoverage]
        private void ExecuteChecks(PropertyInfo property, object objectWithValue, string propertyPath, string serviceName)
        {
            var isPropertyRequired = CheckRequiredPropertyIsNotNull(property, objectWithValue, propertyPath);
            CheckRestrictedCharacterField(property, objectWithValue, propertyPath);
            CheckValueAgainstAllowedValues(property, objectWithValue, isPropertyRequired, propertyPath, serviceName);
            CheckValueAgainstRange(property, objectWithValue, propertyPath);
            CheckClassPropertiesRecursively(property, objectWithValue, isPropertyRequired, propertyPath, serviceName);
        }

        [ExcludeFromCodeCoverage]
        private void CheckClassPropertiesRecursively(PropertyInfo property, object objectWithProperty, bool isRequired, string propertyPath, string serviceName)
        {
            if (!Attribute.IsDefined(property, typeof(DataValidationAttribute)))
            {
                return;
            }

            var objectValue = property.GetValue(objectWithProperty);
            if (!isRequired && objectValue == null)
            {
                return;
            }

            var objectsToCheck = new List<object>();
            if (property.PropertyType.IsArray)
            {
                var array = (object[])objectValue;
                if (!isRequired && array.Length == 0)
                {
                    return;
                }
                objectsToCheck.AddRange((object[])objectValue);
            }
            else
            {
                objectsToCheck.Add(objectValue);
            }

            foreach (var oneObject in objectsToCheck /*possibly childs of an abstract class*/)
            {
                var nonNullPropertyCount = 0;
                foreach (var childProperty in oneObject.GetType().GetProperties() /*that's why we read the props per object - might be different ones*/)
                {
                    ExecuteChecks(childProperty, oneObject, $"{propertyPath}.{childProperty.Name}", serviceName);
                    nonNullPropertyCount += IncrementNonNullPropertyCountIfPropertyIsNotNull(childProperty, oneObject);
                }
                CheckMinimumNonNullPropertyCountIsMet(nonNullPropertyCount, oneObject);
            }
        }

        [ExcludeFromCodeCoverage]
        private void CheckValueAgainstAllowedValues(PropertyInfo property, object objectWithProperty, bool isPropertyRequired, string propertyPath, string serviceName)
        {
            var hasTheAttribute = property.GetCustomAttribute<ValueValidationAttribute>() != null;
            if (hasTheAttribute)
            {
                var isEnum = property.PropertyType.IsEnum;
                var isEnumArray = property.PropertyType.GetElementType()?.IsEnum ?? false;
                if (!isEnum && !isEnumArray)
                {
                    InvalidDtoException.DtoValidationMessage = $" - only enums are supported for this type of validation. The validation attribute {nameof(ValueValidationAttribute)} was incorrectly used in source code for a '{property.PropertyType}' value type. Please contact an administrator if this problem persists. ";
                    throw InvalidDtoException;
                }

                var type = property.PropertyType;
                //var enumsToCheck = new System.Collections.ArrayList();
                //if (property.PropertyType.IsArray)
                //{
                //    var nonArrayType = property.PropertyType.GetElementType();
                //    type = nonArrayType;

                //    dynamic enumArray = property.GetValue(objectWithProperty);
                //    enumsToCheck.AddRange(enumArray);
                //}
                //else
                //{
                //    enumsToCheck.Add(property.GetValue(objectWithProperty));
                //}

                //foreach (var enumToCheck in enumsToCheck)
                //{
                //    if (!Enum.IsDefined(type, enumToCheck) && isPropertyRequired == true)
                //    {
                //        string applicationType = objectWithProperty.GetType().GetProperty("ApplicationType")?.GetValue(objectWithProperty)?.ToString();
                //        if (
                //        serviceName == Constants.EDRSSUBMITSERVICE
                //        && ((applicationType == AppType.COA.ToString()) || (applicationType == AppType.DIS.ToString()))
                //            && enumToCheck.ToString() == "0"
                //            && property.PropertyType == typeof(DocumentCopyType)
                //            )
                //        {
                //            continue;
                //        }
                //        InvalidDtoException.DtoValidationMessage = $" - field {propertyPath} has an invalid [{type}] value: {enumToCheck}";
                //        throw InvalidDtoException;
                //    }
                //}
            }
        }

        [ExcludeFromCodeCoverage]
        private void CheckValueAgainstRange(PropertyInfo property, object objectWithProperty, string propertyPath)
        {
            var attribute = property.GetCustomAttribute<NumberMinMaxValidationAttribute>();
            var hasTheAttribute = attribute != null;
            if (hasTheAttribute)
            {
                var isInt = property.PropertyType == typeof(int);
                var isIntArray = property.PropertyType.GetElementType() == typeof(int);
                var isIntNullable = property.PropertyType == typeof(int?);
                if (!isInt && !isIntArray && !isIntNullable)
                {
                    InvalidDtoException.DtoValidationMessage = $" - only integers are supported for this type of validation. The validation attribute {nameof(NumberMinMaxValidationAttribute)} was incorrectly used in source code for a '{property.PropertyType}' value type. Please contact an administrator if this problem persists. ";

                    throw InvalidDtoException;
                }
                var type = property.PropertyType;
                //var numbersToCheck = new System.Collections.ArrayList();
                //if (property.PropertyType.IsArray)
                //{
                //    var nonArrayType = property.PropertyType.GetElementType();
                //    type = nonArrayType;

                //    dynamic numberArray = property.GetValue(objectWithProperty);
                //    numbersToCheck.AddRange(numberArray);
                //}
                //else
                //{
                //    numbersToCheck.Add(property.GetValue(objectWithProperty));
                //}

                //foreach (int numberToCheck in numbersToCheck)
                //{
                //    if ((attribute.Min.HasValue && numberToCheck < attribute.Min) || (attribute.Max.HasValue && numberToCheck > attribute.Max))
                //    {
                //        //Display range in response message
                //        if (attribute.Min.HasValue
                //            && attribute.Max.HasValue)
                //        {
                //            InvalidDtoException.DtoValidationMessage = $" - field {propertyPath} has an invalid value: {numberToCheck}. Valid range is: {attribute.Min} - {attribute.Max}";
                //            throw InvalidDtoException;
                //        }
                //        //Display minimum value in response message
                //        else if (attribute.Min.HasValue)
                //        {
                //            InvalidDtoException.DtoValidationMessage = $" - field {propertyPath} has an invalid value: {numberToCheck}. The minimum value is {attribute.Min}";
                //            throw InvalidDtoException;
                //        }
                //        //Display maximum value in response message
                //        else if (attribute.Max.HasValue)
                //        {
                //            InvalidDtoException.DtoValidationMessage = $" - field {propertyPath} has an invalid value: {numberToCheck}. The maximum value is {attribute.Max}";
                //            throw InvalidDtoException;
                //        }
                //    }
                //}
            }
        }

        [ExcludeFromCodeCoverage]
        private int IncrementNonNullPropertyCountIfPropertyIsNotNull(PropertyInfo property, object objectWithProperty)
        {
            if (property.GetValue(objectWithProperty) != null)
            {
                return 1;
            }

            return 0;
        }

        [ExcludeFromCodeCoverage]
        private bool CheckRequiredPropertyIsNotNull(PropertyInfo property, object objectWithProperty, string propertyPath)
        {
            if (property.GetCustomAttribute<DataMemberAttribute>()?.IsRequired ?? false)
            {
                if (property.GetValue(objectWithProperty) == null)
                {
                    InvalidDtoException.DtoValidationMessage = $" - mandatory field {propertyPath} not given";
                    throw InvalidDtoException;
                }
                if (string.IsNullOrWhiteSpace(property.GetValue(objectWithProperty)?.ToString()))
                {
                    InvalidDtoException.DtoValidationMessage = $" - mandatory field {propertyPath} not given";
                    throw InvalidDtoException;
                }
                //Validate string arrays
                if (property.GetValue(objectWithProperty).GetType().Equals(typeof(string[])))
                {
                    if ((string[])property.GetValue(objectWithProperty) == null ||
                        ((string[])property.GetValue(objectWithProperty)).Length == 0)
                    {
                        InvalidDtoException.DtoValidationMessage = $" - mandatory field {propertyPath} not given";
                        throw InvalidDtoException;
                    }
                }

                return true;
            }
            return false;
        }

        [ExcludeFromCodeCoverage]
        private void CheckRestrictedCharacterField(PropertyInfo property, object objectWithProperty, string propertyPath)
        {
            if (property.GetValue(objectWithProperty) != null && Attribute.IsDefined(property, typeof(RestrictedCharacterFieldAttribute)))
            {
                var stringsToCheck = new List<string>();
                if (property.PropertyType.IsArray)
                {
                    stringsToCheck.AddRange((string[])property.GetValue(objectWithProperty));
                }
                else
                {
                    stringsToCheck.Add(property.GetValue(objectWithProperty).ToString());
                }

                var regex = property.GetCustomAttribute<RestrictedCharacterFieldAttribute>().RestrictionRegex;
                foreach (var stringTocheck in stringsToCheck)
                {
                    if (!StringHelper.ValidateStringIsRestrictedCharacters(stringTocheck, regex))
                    {
                        InvalidDtoException.DtoValidationMessage = $" - {propertyPath} is not valid with respect to validation constraint: {property.GetCustomAttribute<RestrictedCharacterFieldAttribute>().RestrictionRegex}";
                        throw InvalidDtoException;
                    }
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private void CheckMinimumNonNullPropertyCountIsMet(int nonNullPropertyCount, object objectWithValues)
        {
            var attributeList = objectWithValues.GetType().GetCustomAttributes(true);

            var minimumNonNullPropertyCountAttributeEnumerable = attributeList
                .Where(item => item.GetType() == typeof(MinimumNonNullPropertyCountAttribute))
                .Select(item => item);

            if (minimumNonNullPropertyCountAttributeEnumerable.Any())
            {
                var minimumNonNullPropertyCountAttribute =
                    (MinimumNonNullPropertyCountAttribute)minimumNonNullPropertyCountAttributeEnumerable
                        .First();
                if (minimumNonNullPropertyCountAttribute.MinimumNonNullPropertyCount > nonNullPropertyCount)
                {
                    InvalidDtoException.DtoValidationMessage = " - too many non-null parameters given";
                    throw InvalidDtoException;
                }
            }
        }

        [ExcludeFromCodeCoverage]
        public bool EqualDto(BaseDto otherDto)
        {
            var match = true;
            if (otherDto.GetType() != GetType())
            {
                match = false;
            }

            var thisProperties = GetType().GetProperties();
            var otherProperties = otherDto.GetType().GetProperties();

            for (int i = 0; i < thisProperties.Length; i++)
            {
                //Ignore the DI exception
                if (thisProperties[i].PropertyType == typeof(Exception))
                {
                    continue;
                }
                if (thisProperties[i].GetValue(this) != otherProperties[i].GetValue(otherDto))
                {
                    match = false;
                }
            }

            return match;
        }
    }
}
