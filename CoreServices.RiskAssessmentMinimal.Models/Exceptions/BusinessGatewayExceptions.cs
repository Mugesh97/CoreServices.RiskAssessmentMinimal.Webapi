using Edrs.ActionListenerService.Models.DTO;
using System.Diagnostics.CodeAnalysis;


namespace Edrs.ActionListenerService.Models.Exceptions
{
    [ExcludeFromCodeCoverage]
    public abstract class EDRSException : DtoException
    { }

    [ExcludeFromCodeCoverage]
    public class InvalidEDRSAttachmentParametersException : EDRSException
    {
        public InvalidEDRSAttachmentParametersException() { }
    }

    [ExcludeFromCodeCoverage]
    public abstract class EDRSInvalidParametersException : EDRSException
    { }

    [ExcludeFromCodeCoverage]
    public class InvalidEDRSSubmitParametersException : EDRSInvalidParametersException
    { }

    [ExcludeFromCodeCoverage]
    public class InvalidLogEventsParametersException : DtoException
    {
        public InvalidLogEventsParametersException() { }
        //public InvalidLogEventsParametersException(string message) : base(message) { }
        //public InvalidLogEventsParametersException(string message, Exception innerException) : base(message, innerException) { }
    }
}
