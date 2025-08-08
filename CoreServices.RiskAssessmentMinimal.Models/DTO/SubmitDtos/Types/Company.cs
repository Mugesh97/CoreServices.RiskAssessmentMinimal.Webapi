using Edrs.ActionListenerService.Models.Exceptions;
using Edrs.ActionListenerService.Models.Helpers;
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.SubmitValidations.Party;
using Edrs.ActionListenerService.Models.ModelValidationRules;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
using Edrs.ActionListenerService.ConnectedServices.EDRSSubmit;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(CompanyPartyValidator))]
    public class Company : Party
    {
        [DataMember(IsRequired = true)]
        public string Name { get; set; }

        [DataValidation]
        [DataMember(IsRequired = false)]
        public RegistrationNo RegistrationNumber { get; set; }

        [DataMember(IsRequired = false)]
        public string OverseasTerritory { get; set; }

        [DataMember(IsRequired = false)]
        public string OverseasNumberInTheUnitedKingdom { get; set; }

        public class RegistrationNo
        {
            [JsonConverter(typeof(StringEnumConverter))]
            public enum RegNumberType
            {
                [EDRSEnum(ItemChoiceType.CompanyRegistrationNumber)]
                CompanyRegistration = 1,
                [EDRSEnum(ItemChoiceType.UKLLPRegistrationNumber)]
                UKLLPRegistration = 2
            }

            [ValueValidation]
            [DataMember(IsRequired = true)]
            public RegNumberType NumberType { get; set; }

            [DataMember(IsRequired = true)]
            public string Number { get; set; }
        }

        public override void ValidateParty(JObject jsonObject)
        {
            if (jsonObject.GetValue("registrationNumber", StringComparison.OrdinalIgnoreCase) != null)
            {
                var registrationNumbers = jsonObject.GetValue("registrationNumber", StringComparison.OrdinalIgnoreCase);

                foreach (var registration in registrationNumbers)
                {
                    foreach (var item in registration)
                    {
                        if (item.Path.ToString().ToLower() == "registrationnumber.numbertype")
                        {
                            //Validate the registration number type
                            if (!Enum.IsDefined(typeof(RegistrationNo.RegNumberType), item.ToString()))
                            {
                                var invalidDtoException = new InvalidEDRSSubmitParametersException();

                                var constraint = "must be CompanyRegistration or UKLLPRegistration";
                                invalidDtoException.DtoValidationMessage = $" - {item.Path} is not valid with respect to validation constraint: {constraint}";
                                throw invalidDtoException;
                            }
                        }
                    }
                }

            }

        }
    }
}
