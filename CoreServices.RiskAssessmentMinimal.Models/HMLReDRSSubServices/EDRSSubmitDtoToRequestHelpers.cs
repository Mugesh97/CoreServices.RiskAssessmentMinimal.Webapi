using Edrs.ActionListenerService.ConnectedServices.EDRSSubmit;
using Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types;
using Edrs.ActionListenerService.Models.Exceptions;
using Edrs.ActionListenerService.Models.HMLReDRSSubServices;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using static Edrs.ActionListenerService.Models.Common.Enums;

namespace Landmark.HmlrBg.Core.Services.HMLReDRSSubServices
{
    [ExcludeFromCodeCoverage]
    public static class EDRSSubmitDtoToRequestHelpers
    {
        public static object GetEDRSTitleNumbersFrom(TitleNumbers titles)
        {
            if (titles == null)
            {
                return null;
            }

            if (titles is DealingTitleNumbers)
            {
                var source = (DealingTitleNumbers)titles;
                var serviceType = new DealingType()
                {
                    DealingTitles = ToEDRSTitlesType(source.Titles)
                };
                return serviceType;
            }

            if (titles is LeaseExtensionTitleNumbers)
            {
                var source = (LeaseExtensionTitleNumbers)titles;
                var serviceType = new LeaseExtensionType()
                {
                    LessorTitles = ToEDRSTitlesType(source.Titles),
                    LesseeTitle = source.LesseeTitle,
                    AdditionalTitles = ToEDRSTitlesType(source.AdditionalTitles)
                };
                return serviceType;
            }

            if (titles is NewLeaseTitleNumbers)
            {
                var source = (NewLeaseTitleNumbers)titles;
                var serviceType = new NewLeaseType()
                {
                    LessorTitles = ToEDRSTitlesType(source.Titles),
                    AdditionalTitles = ToEDRSTitlesType(source.AdditionalTitles)
                };
                return serviceType;
            }

            if (titles is TransferOfPartTitleNumbers)
            {
                var source = (TransferOfPartTitleNumbers)titles;
                var serviceType = new TransferOfPartType()
                {
                    TransferorTitles = ToEDRSTitlesType(source.Titles),
                    AdditionalTitles = ToEDRSTitlesType(source.AdditionalTitles)
                };
                return serviceType;
            }

            throw new InvalidEDRSSubmitParametersException();
        }

        private static TitlesType ToEDRSTitlesType(string[] titles)
        {
            if (titles == null || titles.Length == 0)
            {
                return null;
            }

            return new TitlesType()
            {
                TitleNumber = titles
            };
        }

        public static object[] ToEDRSAddressForService(this AddressForDocuments addressForDocuments)
        {
            if (addressForDocuments is SubjectPropertyAddress)
            {
                return new[] { (object)((SubjectPropertyAddress)addressForDocuments).AddressForDocumentsType.ToEDRSEnum<AddressForServiceTypeContent>() };
            }

            if (addressForDocuments is SellersAddress)
            {
                return new[] { (object)((SellersAddress)addressForDocuments).AddressForDocumentsType.ToEDRSEnum<AddressForServiceTypeContent>() };
            }

            if (addressForDocuments is TransferOrAssentAddress)
            {
                return new[] { (object)((TransferOrAssentAddress)addressForDocuments).AddressForDocumentsType.ToEDRSEnum<AddressForServiceTypeContent>() };
            }

            if (addressForDocuments is SpecificAddress)
            {
                var addresses = new List<object>(2);
                var source = (SpecificAddress)addressForDocuments;

                var postalAddress = new PostalAddressType();
                postalAddress = source.PostalAddress.ToeDRSType();

                addresses.Add(postalAddress);
                if (source.AdditionalAddress1 != null || source.AdditionalAddress2 != null)
                {
                    var additional = new AdditionalAddressForServiceType();
                    var additionalAddresses = new List<object>(2);

                    object additionalAddress1 = GetEDRSAddressObject(source.AdditionalAddress1);
                    if (additionalAddress1 != null)
                    {
                        additionalAddresses.Add(additionalAddress1);
                    }
                    object additionalAddress2 = GetEDRSAddressObject(source.AdditionalAddress2);
                    if (additionalAddress2 != null)
                    {
                        additionalAddresses.Add(additionalAddress2);
                    }

                    additional.Items = additionalAddresses.ToArray();

                    addresses.Add(additional);
                }

                return addresses.ToArray();
            }

            throw new InvalidEDRSSubmitParametersException();
        }

        public static PartyRoleType[] ToEDRSType(this Role[] roles, ApplicationBase[] applications)
        {
            if (roles == null || roles.Length == 0)
            {
                return null;
            }

            var edrsRoles = roles.Select(role => new PartyRoleType()
            {
                RoleType = role.Type.ToEDRSEnum<RoleTypeContent>(),
                Priority = applications.FirstOrDefault(application => application.ApplicationId == role.ApplicationId)?.AttachmentId.ToString()
            }).ToArray();
            return edrsRoles;
        }

        public static CompanyType ToEDRSType(this Company company)
        {
            return new CompanyType()
            {
                CompanyName = company.Name,
                ItemElementName = company.RegistrationNumber?.NumberType.ToEDRSEnum<ItemChoiceType>() ?? default(ItemChoiceType),
                Item = company.RegistrationNumber?.Number,
                OverseasTerritory = company.OverseasTerritory,
                OverseasNumberInTheUnitedKingdom = company.OverseasNumberInTheUnitedKingdom
            };
        }

        public static PersonType ToEDRSType(this Person person)
        {
            return new PersonType()
            {
                Forenames = person.Forename,
                Surname = person.Surname
            };
        }

        public static PartyType ToEDRSPartyType(this ApplicationParty applicant, int representativeId, ApplicationBase[] applications)
        {
            var addressForService = default(AddressForServiceType);
            if (applicant.CorrespondenceAddress != null)
            {
                addressForService = new AddressForServiceType()
                {
                    Items = applicant.CorrespondenceAddress.ToEDRSAddressForService()
                };
            }

            var edrsParty = new PartyType
            {
                representativeId = representativeId.ToString(),
                IsApplicant = applicant.IsApplicant,
                Item = applicant.Party.ToEDRSType(),
                Roles = applicant.Party.Roles.ToEDRSType(applications),
                AddressForService = addressForService
            };
            return edrsParty;
        }

        #region Translate Types

        public static object GetEDRSAddressObject(Address source)
        {
            object eDRSAddress = null;
            if (source is MailAddress)
            {
                return GetEDRSAddressObject((MailAddress)source);
            }

            if (source is EmailAddress)
            {
                eDRSAddress = ((EmailAddress)source).ToeDRSType();
            }

            return eDRSAddress;
        }

        public static CareOfAddressType GetEDRSAddressObject(MailAddress source)
        {
            var eDRSAddress = default(CareOfAddressType);
            if (source is PostalAddress)
            {
                eDRSAddress = ((PostalAddress)source).ToeDRSType();
            }
            else if (source is DXAddress)
            {
                eDRSAddress = ((DXAddress)source).ToeDRSType();
            }
            return eDRSAddress;
        }

        public static ApplicationType ToeDRSType(this ApplicationBase source)
        {
            var eDRSApplication = default(ApplicationType);
            if (source is Application)
            {
                eDRSApplication = ((Application)source).ToeDRSType();
            }
            else if (source is ChargeApplication)
            {
                eDRSApplication = ((ChargeApplication)source).ToeDRSType();
            }
            return eDRSApplication;
        }

        public static object ToEDRSType(this Party source)
        {
            var eDRSType = default(object);
            if (source is Person)
            {
                eDRSType = ((Person)source).ToEDRSType();
            }
            else if (source is Company)
            {
                eDRSType = ((Company)source).ToEDRSType();
            }
            return eDRSType;
        }

        public static AdditionalPartyNotificationType ToEDRSType(this AdditionalPartyNotification source)
        {
            var eDRSType = new AdditionalPartyNotificationType()
            {
                Name = source.Name,
                Reference = source.Reference
            };
            var address = GetEDRSAddressObject(source.Address);
            if (address != null)
            {
                eDRSType.Address = new AddressType() { Item = address };
            }

            return eDRSType;
        }

        private static PostalAddressType ToeDRSType(this PostalAddress source)
        {
            var postalAddress = new PostalAddressType
            {
                AddressLine1 = source.AddressLine1,
                AddressLine2 = source.AddressLine2,
                AddressLine3 = source.AddressLine3,
                AddressLine4 = source.AddressLine4,
                City = source.City,
                County = source.County,
                Country = source.Country,
                Postcode = source.Postcode,
            };

            return postalAddress;
        }

        private static DXAddressType ToeDRSType(this DXAddress source)
        {
            var dxAddress = new DXAddressType
            {
                DXNumber = source.DXNumber,
                DXExchange = source.DXExchange
            };

            return dxAddress;
        }

        private static EmailAddressType ToeDRSType(this EmailAddress source)
        {
            var emailAddress = new EmailAddressType
            {
                Email = source.Email
            };

            return emailAddress;
        }

        private static OtherApplicationType ToeDRSType(this Application source)
        {
            var otherApplication = new OtherApplicationType
            {
                Priority = source.AttachmentId.ToString(),
                FeeInPence = source.FeeInPence.ToString(),
                Type = source.ApplicationType.ToEDRSEnum<ApplicationTypeContent>(),
                Value = source.Value
            };

            IEnumerable<int> definedCopyTypeValues = Enum.GetValues(typeof(DocumentCopyType))
                            .OfType<DocumentCopyType>()
                            .Select(s => (int)s);

            if (definedCopyTypeValues.Contains((int)source.CopyType))
            {
                otherApplication.Document = new DocumentType
                {
                    CertifiedCopy = source.CopyType.ToEDRSEnum<CertifiedTypeContent>()
                };
            }

            return otherApplication;
        }

        private static ChargeApplicationType ToeDRSType(this ChargeApplication source)
        {
            var chargeApplication = new ChargeApplicationType
            {
                Priority = source.AttachmentId.ToString(),
                FeeInPence = source.FeeInPence.ToString(),
                Value = source.Value,
                Document = new DocumentType
                {
                    CertifiedCopy = source.CopyType.ToEDRSEnum<CertifiedTypeContent>()
                },
                ChargeDate = source.ChargeDate,
                Item = string.IsNullOrWhiteSpace(source.MDRef) ? new NoMDRefType() : (object)source.MDRef,
                SortCode = source.LendersSortCode
            };

            return chargeApplication;
        }

        public static SupportingDocumentType ToeDRSType(this SupportingDocument source)
        {
            return source.ToeDRSType(source.DocumentName.ToEDRSEnum<DocumentNameContent>());
        }

        public static SupportingDocumentType ToeDRSType(this IdentityForm source)
        {
            return source.ToeDRSType(DocumentNameContent.IdentityForm);
        }

        private static SupportingDocumentType ToeDRSType(this Document source, DocumentNameContent documentName)
        {
            var supportingDoc = new SupportingDocumentType
            {
                CertifiedCopy = source.CopyType.ToEDRSEnum<CertifiedTypeContent>(),
                DocumentId = source.AttachmentId.ToString(),
                DocumentName = documentName
            };
            return supportingDoc;
        }

        public static ApplicationAffectsContent GeteDRSApplicationAffectType(bool? isApplicationAffectWhole)
        {
            var affect = (bool)isApplicationAffectWhole ? ApplicationAffectsContent.WHOLE : ApplicationAffectsContent.PART;
            return affect;
        }

        #endregion
    }
}
