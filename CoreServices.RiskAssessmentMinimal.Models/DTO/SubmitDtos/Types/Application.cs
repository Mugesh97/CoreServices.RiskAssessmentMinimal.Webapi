using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using static Edrs.ActionListenerService.Models.Common.Enums;

namespace Edrs.ActionListenerService.Models.DTO.SubmitDtos.Types
{
    [ExcludeFromCodeCoverage]
    public class Application : ApplicationBase
    {
        public const string JsonNameOfApplicationTypeProperty = "applicationType";
        public const string JsonNameOfCopyTypeProperty = "copyType";

        [ValueValidation()]
        [DataMember(IsRequired = true)]
        public AppType ApplicationType { get; set; }

        public Application(AppType type) : base(Type.Other)
        {
            ApplicationType = type;
        }

        public Application() : this(0) /*non-valid value intentionally to fail at validation later if not provided in json*/
        {
        }
    }
}
