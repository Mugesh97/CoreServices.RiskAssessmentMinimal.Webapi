using Edrs.ActionListenerService.ConnectedServices.EDRSSubmit;
using Edrs.ActionListenerService.Models.Exceptions;
using Edrs.ActionListenerService.Models.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    public abstract class Party
    {
        public const string JsonNameOfTypeProperty = "partyType";

        [JsonConverter(typeof(StringEnumConverter))]
        public enum Type
        {
            Person = 1,
            Company = 2
        }

        [ValueValidation()]
        [DataMember(IsRequired = true)]
        public Type PartyType { get; set; }

        [DataMember(IsRequired = true)]
        public Role[] Roles { get; set; }

        public virtual void ValidateParty(JObject jsonObject)
        {
            //Do specific class validation
        }

        public void ValidateRole(JObject jsonObject)
        {
            CheckForNullOrEmptyRoleProperties(jsonObject);
        }

        public void CheckForNullOrEmptyRoleProperties(JObject jsonObject)
        {
            //Get exception type
            var invalidDtoException = new InvalidEDRSSubmitParametersException();

            var roles = jsonObject.GetValue("roles", StringComparison.OrdinalIgnoreCase);

            if (roles != null)
            {
                foreach (var role in roles)
                {
                    var applicationIdIsPopulated = false;
                    var applicationTypeIsPopulated = false;

                    if (role != null)
                    {
                        foreach (var item in role)
                        {
                            //Validate roles applicationId exists and is populated
                            if (!string.IsNullOrEmpty(item.First.ToString()) && item.Path.ToString().ToLower().Contains("applicationid"))
                            {
                                applicationIdIsPopulated = true;
                            }
                            //Validate roles type exists and is populated
                            else if (!string.IsNullOrEmpty(item.First.ToString()) && item.Path.ToString().ToLower().Contains("type"))
                            {
                                applicationTypeIsPopulated = true;
                                //Validate its of type RoleTypeContent
                                ValidateApplicationType(item.First);
                            }
                        }

                        //Check that both applicationId and type are present, if not, throw exception
                        if (applicationIdIsPopulated == false
                            || applicationTypeIsPopulated == false)
                        {
                            invalidDtoException.DtoValidationMessage = $" - role applicationId and role type must be present for each party.";
                            throw invalidDtoException;
                        }
                    }
                }
            }
        }

        private void ValidateApplicationType(JToken applicationType)
        {
            var invalidDtoException = new InvalidEDRSSubmitParametersException();

            if (!EnumHelper.IsDefinedCaseInsensitive(typeof(RoleTypeContent), applicationType.ToString()))
            {
                invalidDtoException.DtoValidationMessage = $" - role type must be of type RoleTypeContent.";
                throw invalidDtoException;
            }
        }
    }
}
