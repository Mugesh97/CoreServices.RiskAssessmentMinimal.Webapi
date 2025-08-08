using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.Common
{
    [ExcludeFromCodeCoverage]
    public static class Constants
    {
        public const string NonBlankTextTypeRegex = @".*\S.*";
        public const string CaseIdRestrictionRegex = @"^(?=.*\S.*)(?=^.{5,25}$)([a-zA-Z0-9][a-zA-Z0-9\\-]*)";
        public const string NonNegativeIntegerTypeRegex = @"^\d+$";
        public const string ExternalReferenceRestrictionRegex = @"^(?=.*\S.*)(?=^.{1,25}$)";
        public const string ApplicationMessageIdRestrictionRegex = "^([a-zA-Z0-9][a-zA-Z0-9\\-]*){5,50}$";
        public const string AdditionalProviderFilterRestrictionRegex = @"^[a-zA-Z0-9]{0,50}$";
        public const string CertificateName = "CertificateName";
        public const string CertificatePassword = "CertificatePassword";


        public const string EDRSSUBMITSERVICE = "eDRSSubmit";
        public const string EDRSATTACHMENTSERVICE = "eDRSAttachment";
        public const string EdrsAccessDeniedMessage = "Access Denied";
        public const string EdrsAccessLoginInvalid = "Login details are invalid";
        // Full text (which might change): Password must have at least 8 characters and maximum 20 characters with at least 2 numeric characters
        public const string EdrsAccessPasswordNotValid =
            "Password must have at least 8 characters and maximum 20 characters with at least 2 numeric characters";
        public const string EDRSACCESSDENIEDCALLBACKMESSAGE = "Access Denied to HMLR System. Please verify your credentials.";
        public const string EDRSERRORCODE = "hmlrbg.internal.error";
        public const string EdrsInternalServerError = "Internal Server (500) Error";
        public const string ECSSCALLBACKURL = "ECSSCALLBACKURL";
        public const string EDRSHTTPREQUESTFORBIDDEN = "The HTTP request was forbidden with client authentication scheme";
        public const string EDRSNOENDPOINTLISTENING = "There was no endpoint listening at";
        public const string HmlrCertificateNotFound = "Not able to find HMLR Certificate, Please Install Certificate";
        public const string ECSSServiceAppId = "ECSSServiceAppId";
    }
    public static class RoleTypes
    {
        public const string Borrower = "borrower";
        public const string Lender = "lender";
        public const string PersonalRepresentative = "personalRepresentative";
        public const string Proprietor = "proprietor";
        public const string ThirdParty = "thirdParty";
        public const string Transferee = "transferee";
        public const string Transferor = "transferor";
        public const string Lessee = "lessee";
        public const string Lessor = "lessor";
    }
       
    public enum EDRSServiceType : int
    {
        eDRSRequisitionPoll = 107,
        eDRSSubmitPoll = 104,
        eDRSAttachmentPoll = 105,
        eDRSEarlyCompletionPoll = 108
    }

    public enum ResponseType
    {
        Error,
        InvalidCredentials,
        SubmitAck,
        AttachmentAck,
        InternalServerError

    }
    public enum RequestConstant
    {
        Received,
        Submitted,
        Responded,
        ResponseFailure
    }
}
