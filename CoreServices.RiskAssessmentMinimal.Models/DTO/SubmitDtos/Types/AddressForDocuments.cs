using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using Edrs.ActionListenerService.Models.Helpers;
using Edrs.ActionListenerService.ConnectedServices.EDRSSubmit;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    public abstract class AddressForDocuments
    {
        public const string JsonNameOfTypeProperty = "addressForDocumentsType";

        [JsonConverter(typeof(StringEnumConverter))]
        public enum Type
        {
            [EDRSEnum(AddressForServiceTypeContent.A1)]
            SubjectPropertyAddress = 1,
            [EDRSEnum(AddressForServiceTypeContent.B1)]
            SellersAddress = 2,
            [EDRSEnum(AddressForServiceTypeContent.TA)]
            TransferOrAssentAddress = 3,
            SpecificAddress = 4
        }

        [ValueValidation()]
        [DataMember(IsRequired = true)]
        public Type AddressForDocumentsType { get; set; }

        public AddressForDocuments(Type type)
        {
            AddressForDocumentsType = type;
        }
    }
}
