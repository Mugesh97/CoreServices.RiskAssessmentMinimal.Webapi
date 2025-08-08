using Edrs.ActionListenerService.ConnectedServices.EDRSAttachment;
using Edrs.ActionListenerService.Models.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.Common
{
    [ExcludeFromCodeCoverage]
    public class Enums
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum DocumentNameType
        {
            [EDRSEnum(DocumentNameContent.Abstract)]
            Abstract = 1,

            [EDRSEnum(DocumentNameContent.Agreement)]
            Agreement = 2,

            [EDRSEnum(DocumentNameContent.Assignment)]
            Assignment = 3,

            [EDRSEnum(DocumentNameContent.Conveyance)]
            Conveyance = 4,

            [EDRSEnum(DocumentNameContent.Correspondence)]
            Correspondence = 5,

            [EDRSEnum(DocumentNameContent.CourtOrder)]
            CourtOrder = 6,

            [EDRSEnum(DocumentNameContent.Deed)]
            Deed = 7,

            [EDRSEnum(DocumentNameContent.FormDI)]
            FormDI = 8,

            [EDRSEnum(DocumentNameContent.DocumentList)]
            DocumentList = 9,

            [EDRSEnum(DocumentNameContent.Evidence)]
            Evidence = 10,

            [EDRSEnum(DocumentNameContent.EX1A)]
            EX1A = 11,

            [EDRSEnum(DocumentNameContent.IdentityEvidence)]
            IdentityEvidence = 12,

            [EDRSEnum(DocumentNameContent.IdentityForm)] //Commented out as it is being implemented elsewhere.
            IdentityForm = 13,

            [EDRSEnum(DocumentNameContent.Indenture)]
            Indenture = 14,

            [EDRSEnum(DocumentNameContent.LandTransactionTax)]
            LandTransactionTax = 15,

            [EDRSEnum(DocumentNameContent.Lease)]
            Lease = 16,

            [EDRSEnum(DocumentNameContent.Licence)]
            Licence = 17,

            [EDRSEnum(DocumentNameContent.LRCorrespondence)]
            LRCorrespondence = 18,

            [EDRSEnum(DocumentNameContent.PowerofAttorney)]
            PowerofAttorney = 19,

            [EDRSEnum(DocumentNameContent.StampDutyLandTax)]
            StampDutyLandTax = 20,

            [EDRSEnum(DocumentNameContent.StatementOfTruth)]
            StatementOfTruth = 21,

            [EDRSEnum(DocumentNameContent.StatutoryDeclaration)]
            StatutoryDeclaration = 22,

            [EDRSEnum(DocumentNameContent.WitnessStatement)]
            WitnessStatement = 23,

            [EDRSEnum(DocumentNameContent.Assent)]
            Assent = 24,

            [EDRSEnum(DocumentNameContent.BirthCertificate)]
            BirthCertificate = 25,

            [EDRSEnum(DocumentNameContent.Charge)]
            Charge = 26,

            [EDRSEnum(DocumentNameContent.DeathCertificate)]
            DeathCertificate = 27,

            [EDRSEnum(DocumentNameContent.Discharge)]
            Discharge = 28,

            [EDRSEnum(DocumentNameContent.MarriageCertificate)]
            MarriageCertificate = 29,

            [EDRSEnum(DocumentNameContent.Probate)]
            Probate = 30,

            [EDRSEnum(DocumentNameContent.SubCharge)]
            SubCharge = 31,

            [EDRSEnum(DocumentNameContent.Transfer)]
            Transfer = 32,

            [EDRSEnum(DocumentNameContent.Plan)]
            Plan = 33
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum DocumentCopyType
        {
            [EDRSEnum(CertifiedTypeContent.Original)]
            Original = 1,
            [EDRSEnum(CertifiedTypeContent.Certified)]
            Certified = 2,
            [EDRSEnum(CertifiedTypeContent.Scanned)]
            Scanned = 3
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum AppType // Ignore the alphabetical ordering.
        {
            [EDRSEnum(ApplicationTypeContent.AN1)]
            AN1 = 1,

            [EDRSEnum(ApplicationTypeContent.AP1)]
            AP1 = 2,

            [EDRSEnum(ApplicationTypeContent.APT)]
            APT = 3,

            [EDRSEnum(ApplicationTypeContent.AS1)]
            AS1 = 4,

            [EDRSEnum(ApplicationTypeContent.AS2)]
            AS2 = 5,

            [EDRSEnum(ApplicationTypeContent.AS3)]
            AS3 = 6,

            // CAG Removed in version 2.1

            [EDRSEnum(ApplicationTypeContent.CCD)]
            CCD = 8,

            [EDRSEnum(ApplicationTypeContent.CH2)]
            CH2 = 9,

            [EDRSEnum(ApplicationTypeContent.CH3)]
            CH3 = 10,

            [EDRSEnum(ApplicationTypeContent.CN)]
            CN = 11,

            [EDRSEnum(ApplicationTypeContent.CNL)]
            CNL = 12,

            [EDRSEnum(ApplicationTypeContent.COA)]
            COA = 13,

            [EDRSEnum(ApplicationTypeContent.CPD)]
            CPD = 14,

            [EDRSEnum(ApplicationTypeContent.DIS)]
            DIS = 15,

            [EDRSEnum(ApplicationTypeContent.DJP)]
            DJP = 16,

            [EDRSEnum(ApplicationTypeContent.DSP)]
            DSP = 17,

            [EDRSEnum(ApplicationTypeContent.EX1)]
            EX1 = 18,

            [EDRSEnum(ApplicationTypeContent.EX3)]
            EX3 = 19,

            [EDRSEnum(ApplicationTypeContent.HR1)]
            HR1 = 20,

            [EDRSEnum(ApplicationTypeContent.HR2)]
            HR2 = 21,

            [EDRSEnum(ApplicationTypeContent.HR4)]
            HR4 = 22,

            [EDRSEnum(ApplicationTypeContent.LEASE)]
            LEASE = 23,

            [EDRSEnum(ApplicationTypeContent.NOE)]
            NOE = 24,

            [EDRSEnum(ApplicationTypeContent.NOL)]
            NOL = 25,

            [EDRSEnum(ApplicationTypeContent.PC)]
            PC = 26,

            [EDRSEnum(ApplicationTypeContent.RC)]
            RC = 27,

            [EDRSEnum(ApplicationTypeContent.RFN)]
            RFN = 28,

            [EDRSEnum(ApplicationTypeContent.ROCA)]
            ROCA = 29,

            [EDRSEnum(ApplicationTypeContent.ROCC)]
            ROCC = 30,

            [EDRSEnum(ApplicationTypeContent.ROCU)]
            ROCU = 31,

            [EDRSEnum(ApplicationTypeContent.ROE)]
            ROE = 32,

            [EDRSEnum(ApplicationTypeContent.RX1)]
            RX1 = 33,

            [EDRSEnum(ApplicationTypeContent.RX2)]
            RX2 = 34,

            [EDRSEnum(ApplicationTypeContent.RX3)]
            RX3 = 35,

            [EDRSEnum(ApplicationTypeContent.RX4)]
            RX4 = 36,

            [EDRSEnum(ApplicationTypeContent.SBC)]
            SBC = 37,

            [EDRSEnum(ApplicationTypeContent.SEV)]
            SEV = 38,

            [EDRSEnum(ApplicationTypeContent.SL)]
            SL = 39,

            [EDRSEnum(ApplicationTypeContent.TNV)]
            TNV = 40,

            [EDRSEnum(ApplicationTypeContent.TP1)]
            TP1 = 41,

            [EDRSEnum(ApplicationTypeContent.TP2)]
            TP2 = 42,

            [EDRSEnum(ApplicationTypeContent.TR1)]
            TR1 = 43,

            [EDRSEnum(ApplicationTypeContent.TR2)]
            TR2 = 44,

            [EDRSEnum(ApplicationTypeContent.TR4)]
            TR4 = 45,

            [EDRSEnum(ApplicationTypeContent.TRM)]
            TRM = 46,

            [EDRSEnum(ApplicationTypeContent.TSC)]
            TSC = 47,

            [EDRSEnum(ApplicationTypeContent.UN1)]
            UN1 = 48,

            [EDRSEnum(ApplicationTypeContent.UN2)]
            UN2 = 49,

            [EDRSEnum(ApplicationTypeContent.UN3)]
            UN3 = 50,

            [EDRSEnum(ApplicationTypeContent.UN4)]
            UN4 = 51,

            [EDRSEnum(ApplicationTypeContent.UT1)]
            UT1 = 52,

            [EDRSEnum(ApplicationTypeContent.VC)]
            VC = 53,

            [EDRSEnum(ApplicationTypeContent.VLAN)]
            VLAN = 54,

            [EDRSEnum(ApplicationTypeContent.VLAP)]
            VLAP = 55,

            [EDRSEnum(ApplicationTypeContent.VLUN)]
            VLUN = 56,

            [EDRSEnum(ApplicationTypeContent.VOCA)]
            VOCA = 57,

            [EDRSEnum(ApplicationTypeContent.VOCU)]
            VOCU = 58,

            [EDRSEnum(ApplicationTypeContent.VOE)]
            VOE = 59,

            [EDRSEnum(ApplicationTypeContent.VOEA)]
            VOEA = 60,

            [EDRSEnum(ApplicationTypeContent.VOEU)]
            VOEU = 61,

            [EDRSEnum(ApplicationTypeContent.WCT)]
            WCT = 62,

            [EDRSEnum(ApplicationTypeContent.ADV1)]
            ADV1 = 63,

            [EDRSEnum(ApplicationTypeContent.ADV2)]
            ADV2 = 64,

            [EDRSEnum(ApplicationTypeContent.CN1)]
            CN1 = 65,

            [EDRSEnum(ApplicationTypeContent.DB)]
            DB = 66,

            [EDRSEnum(ApplicationTypeContent.DOL)]
            DOL = 67,

            [EDRSEnum(ApplicationTypeContent.RGOE)]
            RGOE = 68,

            [EDRSEnum(ApplicationTypeContent.SC)]
            SC = 69
        }
    }

    public enum GatewayResponseLocalNames
    {
        [EDRSEnum(ProductResponseCodeContentType.Item0)]
        Other,
        [EDRSEnum(ProductResponseCodeContentType.Item10)]
        Acknowledgement,
        [EDRSEnum(ProductResponseCodeContentType.Item20)]
        Rejection,
        [EDRSEnum(ProductResponseCodeContentType.Item30)]
        Result,
    }
}
