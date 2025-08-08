using Edrs.ActionListenerService.ConnectedServices.EDRSSubmit;
using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using Landmark.HmlrBg.Core.Services.HMLReDRSSubServices;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Edrs.ActionListenerService.Models.HMLReDRSSubServices
{
    [ExcludeFromCodeCoverage]
    public class EDRSSubmitRequestBuilder
    {
        public RequestApplicationToChangeRegisterV2_1Type Build()
        {
            int representativeId = 1;

            var conveyancer = new LodgingConveyancerType();
            conveyancer.RepresentativeId = representativeId.ToString();
            var allParties = _submittingConveyancer?.Representees?.Select(representee => representee.ToEDRSPartyType(representativeId, _apps)).ToList() ?? new List<PartyType>();
            representativeId++;

            var allSupportingDocs = _supportingDocs?.Select(doc => doc.ToeDRSType());
            IdentityEvidenceType evidence = null;
            if (_partiesVerifiedByIdForms != null && _partiesVerifiedByIdForms.Length > 0)
            {
                evidence = new IdentityEvidenceType();
                evidence.RepresentativeId = representativeId.ToString();
                 allParties = allParties.Concat(_partiesVerifiedByIdForms.Select(representee => representee.ToEDRSPartyType(representativeId, _apps))).ToList();
                allSupportingDocs = allSupportingDocs.Concat(_partiesVerifiedByIdForms.Select(representee => representee.IdentityForm?.ToeDRSType()));
                representativeId++;
            }

            CertifiedType certified = null;
            if (_partiesVerifiedWithoutIdForms != null && _partiesVerifiedWithoutIdForms.Length > 0)
            {
                certified = new CertifiedType();
                certified.RepresentativeId = representativeId.ToString();
                allParties = allParties.Concat(_partiesVerifiedWithoutIdForms.Select(representee => representee.ToEDRSPartyType(representativeId, _apps))).ToList();
                representativeId++;
            }

            List<RepresentingConveyancerType> representingConveyancers = null;
            if (_otherConveyancers != null && _otherConveyancers.Length > 0)
            {
                representingConveyancers = new List<RepresentingConveyancerType>(_otherConveyancers.Length);
                foreach (var dtoConveyancer in _otherConveyancers)
                {
                    var otherConveyancer = new RepresentingConveyancerType
                    {
                        RepresentativeId = representativeId.ToString(),
                        ConveyancerName = dtoConveyancer.Name,
                        Reference = dtoConveyancer.Reference,
                        Item = EDRSSubmitDtoToRequestHelpers.GetEDRSAddressObject(dtoConveyancer.Address)
                    };
                    representingConveyancers.Add(otherConveyancer);

                    if (dtoConveyancer.Representees != null && dtoConveyancer.Representees.Length > 0)
                    {
                        allParties = allParties.Concat(dtoConveyancer.Representees.Select(representee => representee.ToEDRSPartyType(representativeId, _apps))).ToList();
                    }

                    representativeId++;
                }
            }

            return new RequestApplicationToChangeRegisterV2_1Type()
            {
                MessageId = _messageId,
                ExternalReference = _externalReference,
                AdditionalProviderFilter = _additionalProviderFilter,

                Product = new ProductType()
                {
                    Reference = _reference,
                    TotalFeeInPence = _totalFeeInPence,
                    Email = _email,
                    TelephoneNumber = _telephoneNumber,
                    DisclosableOveridingInterests = (bool)_disclosableOveridingInterests,
                    ApplicationDate = (DateTime)_applicationDate,
                    AP1WarningUnderstood = (bool)_AP1WarningUnderstood,
                    LocalAuthority = _localAuthority,
                    PostcodeOfProperty = _propertyPostcode,
                    Titles = new ServiceTitlesType() { Item = EDRSSubmitDtoToRequestHelpers.GetEDRSTitleNumbersFrom(_titles) },
                    ApplicationAffects = EDRSSubmitDtoToRequestHelpers.GeteDRSApplicationAffectType(_isApplicationAffectWhole),

                    Applications = _apps == null || _apps.Length == 0 ? null : _apps.Select(app => app.ToeDRSType()).ToArray(),
                    SupportingDocuments = allSupportingDocs == null || allSupportingDocs.Count() == 0 ? null : new SupportingDocumentsType { SupportingDocument = allSupportingDocs.ToArray() },

                    Representations = new RepresentationsType()
                    {
                        LodgingConveyancer = conveyancer,
                        IdentityEvidence = evidence,
                        Certified = certified,
                        RepresentingConveyancer = representingConveyancers?.ToArray()
                    },

                    Parties = new PartiesType()
                    {
                        Party = allParties?.ToArray()
                    },

                    AdditionalPartyNotifications = _additionalPartyNotifications == null || _additionalPartyNotifications.Length == 0 ? null :
                    new AdditionalPartyNotificationsType()
                    {
                        AdditionalPartyNotification = _additionalPartyNotifications.Select(notif => notif.ToEDRSType()).ToArray()
                    }
                }
            };
        }

        public EDRSSubmitRequestBuilder Applications(ApplicationBase[] apps)
        {
            _apps = apps ?? new ApplicationBase[0] { };
            return this;
        }


        public EDRSSubmitRequestBuilder MessageId(string messageId)
        {
            _messageId = messageId ?? string.Empty;
            return this;
        }

        public EDRSSubmitRequestBuilder ExternalReference(string externalReference)
        {
            _externalReference = externalReference ?? string.Empty;
            return this;
        }


        public EDRSSubmitRequestBuilder AdditionalProviderFilter(string additionalProviderFilter)
        {
            _additionalProviderFilter = additionalProviderFilter ?? string.Empty;
            return this;
        }

        public EDRSSubmitRequestBuilder Reference(string reference)
        {
            _reference = reference;
            return this;
        }

        public EDRSSubmitRequestBuilder TotalFeeInPence(string totalFeeInPence)
        {
            _totalFeeInPence = totalFeeInPence ?? string.Empty;
            return this;
        }

        public EDRSSubmitRequestBuilder Email(string email)
        {
            _email = email;
            return this;
        }

        public EDRSSubmitRequestBuilder TelephoneNumber(string telephoneNumber)
        {
            _telephoneNumber = telephoneNumber;
            return this;
        }

        public EDRSSubmitRequestBuilder DisclosableOveridingInterests(bool? disclosableOveridingInterests)
        {
            _disclosableOveridingInterests = disclosableOveridingInterests;
            return this;
        }

        public EDRSSubmitRequestBuilder ApplicationDate(DateTime? applicationDate)
        {
            _applicationDate = applicationDate;
            return this;
        }

        public EDRSSubmitRequestBuilder AP1WarningUnderstood(bool? isAP1WarningUnderstood)
        {
            _AP1WarningUnderstood = isAP1WarningUnderstood;
            return this;
        }

        public EDRSSubmitRequestBuilder LocalAuthority(string localAuthority)
        {
            _localAuthority = localAuthority;
            return this;
        }

        public EDRSSubmitRequestBuilder PropertyPostcode(string propertyPostcode)
        {
            _propertyPostcode = propertyPostcode;
            return this;
        }

        public EDRSSubmitRequestBuilder ApplicationAffect(bool? isApplicationAffectWhole)
        {
            _isApplicationAffectWhole = isApplicationAffectWhole;
            return this;
        }

        public EDRSSubmitRequestBuilder Titles(TitleNumbers titles)
        {
            _titles = titles;
            return this;
        }

        public EDRSSubmitRequestBuilder SupportingDocuments(SupportingDocument[] supportingDocs)
        {
            _supportingDocs = supportingDocs;
            return this;
        }


        public EDRSSubmitRequestBuilder SubmittingConveyancer(SubmittingConveyancer submittingConveyancer)
        {
            _submittingConveyancer = submittingConveyancer;
            return this;
        }

        public EDRSSubmitRequestBuilder OtherConveyancers(OtherConveyancer[] otherConveyancers)
        {
            _otherConveyancers = otherConveyancers;
            return this;
        }

        public EDRSSubmitRequestBuilder PartiesVerifiedByIdForms(PartyVerifiedByIdForm[] partiesVerifiedByIdForms)
        {
            _partiesVerifiedByIdForms = partiesVerifiedByIdForms;
            return this;
        }

        public EDRSSubmitRequestBuilder PartiesVerifiedWithoutIdForms(ApplicationParty[] partiesVerifiedWithoutIdForms)
        {
            _partiesVerifiedWithoutIdForms = partiesVerifiedWithoutIdForms;
            return this;
        }

        public EDRSSubmitRequestBuilder AdditionalPartyNotifications(AdditionalPartyNotification[] additionalPartyNotifications)
        {
            _additionalPartyNotifications = additionalPartyNotifications ?? new AdditionalPartyNotification[0];
            return this;
        }

        public EDRSSubmitRequestBuilder()
        {
        }

        ApplicationBase[] _apps;
        string _messageId;
        string _externalReference;
        string _additionalProviderFilter;
        string _reference;
        string _totalFeeInPence;
        string _email;
        string _telephoneNumber;
        bool? _disclosableOveridingInterests;
        DateTime? _applicationDate;
        bool? _AP1WarningUnderstood;
        string _localAuthority;
        string _propertyPostcode;
        bool? _isApplicationAffectWhole;
        TitleNumbers _titles;
        SupportingDocument[] _supportingDocs;
        SubmittingConveyancer _submittingConveyancer;
        OtherConveyancer[] _otherConveyancers;
        PartyVerifiedByIdForm[] _partiesVerifiedByIdForms;
        ApplicationParty[] _partiesVerifiedWithoutIdForms;
        AdditionalPartyNotification[] _additionalPartyNotifications;
    }
}