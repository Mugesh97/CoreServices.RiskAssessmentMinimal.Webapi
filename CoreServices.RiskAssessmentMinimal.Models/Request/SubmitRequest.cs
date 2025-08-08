using Edrs.ActionListenerService.Models.DTO;
using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Converters;
using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using Edrs.ActionListenerService.Models.ModelValidationRules;
using Edrs.ActionListenerService.Models.ModelValidationRules.Validators.AttachmentValidations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;

namespace Edrs.ActionListenerService.Models.Request
{
    [ExcludeFromCodeCoverage]
    [Validator(typeof(SubmitRequestValidator))]
    public class SubmitRequest : CommonRequestModel
    {
        public const string NonBlankTextTypeRegex = @".*\S.*";
        public const string CaseIdRestrictionRegex = @"^(?=.*\S.*)(?=^.{5,25}$)([a-zA-Z0-9][a-zA-Z0-9\\-]*)";
        public const string NonNegativeIntegerTypeRegex = @"^\d+$";

        public SubmitRequest()
        {
        }

        [DataMember(IsRequired = true)]
        public string CaseId { get; set; }
        [DataMember(IsRequired = true)]
        public string CaseManagementSystemReference { get; set; }

        [JsonConverter(typeof(BoolValidatorConverter))]
        [DataMember(IsRequired = true)]
        public bool IsAP1WarningUnderstood { get; set; }
        [DataMember(IsRequired = true)]
        public string TotalFeeInPence { get; set; }
        [DataMember(IsRequired = true)]
        public DateTime? ApplicationDate { get; set; }
        [DataMember(IsRequired = false)]
        public string LocalAuthority { get; set; }

        [DataMember(IsRequired = false)]
        public string PropertyPostcode { get; set; }

        [JsonConverter(typeof(TitleNumbersConverter))]
        [DataValidation]
        [DataMember(IsRequired = false)]
        public TitleNumbers TitleNumbers { get; set; }

        [JsonConverter(typeof(BoolValidatorConverter))]
        [DataMember(IsRequired = true)]
        public bool IsApplicationAffectWhole { get; set; }

        [JsonConverter(typeof(BoolValidatorConverter))]
        [DataMember(IsRequired = true)]
        public bool DisclosableOveridingInterests { get; set; }


        [DataValidation()]
        [DataMember(IsRequired = true)]
        public SubmittingConveyancer SubmittingConveyancer { get; set; }


        [DataMember(IsRequired = false)]
        public OtherConveyancer[] OtherConveyancers { get; set; }

        [DataValidation()]
        [DataMember(IsRequired = false)]
        public UnrepresentedParties UnrepresentedParties { get; set; }

        [JsonConverter(typeof(ApplicationConverter))]
        [DataValidation()]
        [DataMember(IsRequired = true)]
        public ApplicationBase[] Applications { get; set; }

        [JsonConverter(typeof(SupportingDocumentConverter))]
        [DataValidation()]
        [DataMember(IsRequired = false)]
        public SupportingDocument[] SupportingDocuments { get; set; }

        [DataValidation()]
        [DataMember(IsRequired = false)]
        public AdditionalPartyNotification[] AdditionalPartyNotifications { get; set; }


        public void PerformDtoSpecificValidation()
        {
            ValidateAttachmentIds();
            ValidateConveyancerProperties();
        }

        #region Private Helper(s)

        private void ValidateAttachmentIds()
        {
            var attachmentIds = new List<int?>(); // nullable  just to make linq happy and co-operate
            var supportingDocAttachmentIds = SupportingDocuments?.Select(doc => doc?.AttachmentId).ToList();

            if (supportingDocAttachmentIds != null)
            {
                foreach (var supportingDocAttachmentId in supportingDocAttachmentIds)
                {
                    if (supportingDocAttachmentId != null)
                    {
                        attachmentIds.Add(supportingDocAttachmentId);
                    }
                }
            }

            var identityFormAttachmentIds = UnrepresentedParties?.PartiesVerifiedByIdForms?.Select(party => party?.IdentityForm?.AttachmentId).ToList();
            if (identityFormAttachmentIds != null)
            {
                foreach (var identityFormAttachmentId in identityFormAttachmentIds)
                {
                    if (identityFormAttachmentId != null)
                    {
                        attachmentIds.Add(identityFormAttachmentId);
                    }
                }
            }

            var appAttachmentIds = Applications?.Select(app => app?.AttachmentId).ToList();
            if (appAttachmentIds != null)
            {
                foreach (var appAttachmentId in appAttachmentIds)
                {
                    if (appAttachmentId != null)
                    {
                        attachmentIds.Add(appAttachmentId);
                    }
                }
            }

            var attachmentIDs = attachmentIds.Where(id => id != null).ToList();

            ValidateAttachmentIdsAreUnique(ref attachmentIDs);
            ValidateAttachmentIdsAreInConsecutiveNumbericalOrder(ref attachmentIDs);
        }

        private void ValidateAttachmentIdsAreUnique(ref List<int?> attachmentIdsList)
        {
            var wereDuplicatesFound = attachmentIdsList.Count != attachmentIdsList.Distinct().Count();
            if (wereDuplicatesFound)
            {
                InvalidDtoException.DtoValidationMessage = " - duplicate attachmentIds found.";
                throw InvalidDtoException;
            }
        }

        private void ValidateAttachmentIdsAreInConsecutiveNumbericalOrder(ref List<int?> attachmentIdsList)
        {
            //Sort into numerical order
            attachmentIdsList.Sort();

            //Check if attachment Ids are in consecutive order
            if (attachmentIdsList.Select((i, j) => i - j).Distinct().Skip(1).Any() == true)
            {
                InvalidDtoException.DtoValidationMessage = " - attachmentIds must be in numerical consecutive order.";
                throw InvalidDtoException;
            }
        }

        private void ValidateConveyancerProperties()
        {
            //Get attachment application Id
            var attachmentApplicationIds = Applications?.Select(doc => doc?.ApplicationId).ToList();

            //Create checklists to loop through later
            List<string> allPartyRoleApplicationIdCheckList = new List<string>();
            List<Role> allPartyRoleCheckList = new List<Role>();
            List<object> representeesCheckList = new List<object>();

            //Validate all conveyancer types
            ValidateSubmittingConveyancers(ref allPartyRoleApplicationIdCheckList, ref allPartyRoleCheckList, ref representeesCheckList);
            ValidateUnrepresentedPartiesVerifiedByIdForms(ref allPartyRoleApplicationIdCheckList, ref allPartyRoleCheckList, ref representeesCheckList);
            ValidateUnrepresentedPartiesVerifiedWithoutIdForms(ref allPartyRoleApplicationIdCheckList, ref allPartyRoleCheckList, ref representeesCheckList);
            ValidateOtherConveyancers(ref allPartyRoleApplicationIdCheckList, ref allPartyRoleCheckList, ref representeesCheckList);

            ValidateApplicationIds(ref allPartyRoleApplicationIdCheckList, ref attachmentApplicationIds);
            ValidateRepresentees(ref representeesCheckList);
        }

        public void ValidateSubmittingConveyancers(ref List<string> allPartyRoleApplicationIdCheckList, ref List<Role> allPartyRoleCheckList, ref List<object> representeesCheckList)
        {
            if (SubmittingConveyancer?.Representees != null)
            {
                //Get checklists from parties
                var submittingPartyRoles = SubmittingConveyancer?.Representees?.Select(party => party?.Party?.Roles).ToList();
                if (submittingPartyRoles != null)
                {
                    GetCheckListsFromParties(submittingPartyRoles, ref allPartyRoleApplicationIdCheckList);
                }

                //Validate representees
                var submittingPartyApplicants = SubmittingConveyancer?.Representees?.Select(app => app?.IsApplicant);
                if (submittingPartyApplicants != null)
                {
                    foreach (var applicant in submittingPartyApplicants)
                    {
                        representeesCheckList.Add(applicant);
                    }
                }
            }
        }

        public void ValidateUnrepresentedPartiesVerifiedByIdForms(ref List<string> allPartyRoleApplicationIdCheckList, ref List<Role> allPartyRoleCheckList, ref List<object> representeesCheckList)
        {
            //Get checklists from parties
            var unrepresentedPartyRolesVerifiedByIdForms = UnrepresentedParties?.PartiesVerifiedByIdForms?.Select(party => party?.Party?.Roles).ToList();
            if (unrepresentedPartyRolesVerifiedByIdForms != null)
            {
                GetCheckListsFromParties(unrepresentedPartyRolesVerifiedByIdForms, ref allPartyRoleApplicationIdCheckList);
            }

            //Validate representees
            var unrepresentedPartyRolesVerifiedByIdFormsApplicants = UnrepresentedParties?.PartiesVerifiedByIdForms?.Select(app => app.IsApplicant);
            if (unrepresentedPartyRolesVerifiedByIdFormsApplicants != null)
            {
                foreach (var applicant in unrepresentedPartyRolesVerifiedByIdFormsApplicants)
                {
                    representeesCheckList.Add(applicant);
                }
            }
        }

        public void ValidateUnrepresentedPartiesVerifiedWithoutIdForms(ref List<string> allPartyRoleApplicationIdCheckList, ref List<Role> allPartyRoleCheckList, ref List<object> representeesCheckList)
        {
            //Get checklists from parties
            var unrepresentedPartyRolesVerifiedWithoutIdForms = UnrepresentedParties?.PartiesVerifiedWithoutIdForms?.Select(party => party?.Party?.Roles).ToList();
            if (unrepresentedPartyRolesVerifiedWithoutIdForms != null)
            {
                GetCheckListsFromParties(unrepresentedPartyRolesVerifiedWithoutIdForms, ref allPartyRoleApplicationIdCheckList);
            }

            //Validate representees
            var unrepresentedPartyRolesVerifiedWithoutIdFormsApplicants = UnrepresentedParties?.PartiesVerifiedWithoutIdForms?.Select(app => app.IsApplicant);
            if (unrepresentedPartyRolesVerifiedWithoutIdFormsApplicants != null)
            {
                foreach (var applicant in unrepresentedPartyRolesVerifiedWithoutIdFormsApplicants)
                {
                    representeesCheckList.Add(applicant);
                }
            }
        }

        public void ValidateOtherConveyancers(ref List<string> allPartyRoleApplicationIdCheckList, ref List<Role> allPartyRoleCheckList, ref List<object> representeesCheckList)
        {
            if (OtherConveyancers != null)
            {
                foreach (var conveyancer in OtherConveyancers)
                {
                    if (conveyancer?.Representees != null)
                    {
                        //Get checklists from parties
                        var otherConveyancerPartyRoles = conveyancer?.Representees?.Select(party => party?.Party?.Roles).ToList();
                        if (otherConveyancerPartyRoles != null)
                        {
                            GetCheckListsFromParties(otherConveyancerPartyRoles, ref allPartyRoleApplicationIdCheckList);
                        }
                        //Validate representees
                        var otherConveyancerPartyApplicants = conveyancer?.Representees?.Select(app => app?.IsApplicant);
                        foreach (var applicant in otherConveyancerPartyApplicants)
                        {
                            representeesCheckList.Add(applicant);
                        }
                    }
                }
            }
        }

        public void GetCheckListsFromParties(List<Role[]> partyRoles, ref List<string> allPartyApplicationIdCheckList)
        {
            foreach (var roles in partyRoles)
            {
                ValidateRolesAreUnique(roles, ref allPartyApplicationIdCheckList);
            }
        }

        public void ValidateApplicationIds(ref List<string> partyApplicationIds, ref List<string> attachmentApplicationIds)
        {
            if (attachmentApplicationIds != null)
            {
                foreach (var partyApplicationId in partyApplicationIds)
                {
                    //Check role application Id has a corresponding attachment application Id
                    if (!attachmentApplicationIds.Contains(partyApplicationId))
                    {
                        InvalidDtoException.DtoValidationMessage = $" - applicationId: {partyApplicationId} doesn't have a corresponding application.";
                        throw InvalidDtoException;
                    }

                    //Attempt to get the application attachment Id
                    foreach (var application in Applications)
                    {
                        if (application.ApplicationId == partyApplicationId)
                        {
                            var attachmentId = application.AttachmentId;

                            if (String.IsNullOrEmpty(attachmentId.ToString()))
                            {
                                InvalidDtoException.DtoValidationMessage = " - attachmentId must be provided for an application.";
                                throw InvalidDtoException;
                            }
                        }
                    }
                }
            }
        }

        public void ValidateRolesAreUnique(Role[] partyRoles, ref List<string> allPartyApplicationIdCheckList)
        {
            //Create new checklist instance for each party role
            List<Role> allPartyRoleCheckList = new List<Role>();
            foreach (var partyRole in partyRoles)
            {
                foreach (var roles in allPartyRoleCheckList)
                {
                    if (roles.ApplicationId == partyRole.ApplicationId
                    && roles.Type == partyRole.Type)
                    {
                        InvalidDtoException.DtoValidationMessage = " - duplicate unique value declared for identity constraint 'Roles'.";
                        throw InvalidDtoException;
                    }
                }
                allPartyRoleCheckList.Add(partyRole);
                allPartyApplicationIdCheckList.Add(partyRole.ApplicationId);
            }
        }

        public void ValidateRepresentees(ref List<object> representeesCheckList)
        {
            if (representeesCheckList.Count > 0)
            {
                ValidateApplicantExists(ref representeesCheckList);
            }
        }

        public void ValidateApplicantExists(ref List<object> representeesCheckList)
        {
            if (!representeesCheckList.Contains(true))
            {
                InvalidDtoException.DtoValidationMessage = " - at least one party must be an applicant.";
                throw InvalidDtoException;
            }
        }

        #endregion
    }
}
