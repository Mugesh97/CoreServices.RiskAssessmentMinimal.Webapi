using Core.Services.Libraries.Base.Lgs;
using Newtonsoft.Json.Schema;

namespace CoreServices.RiskAssessmentMinimal.Webapi.SchemaValidation
{
    public interface IValidationErrorsFormatter
    {
        List<LgsMessageDto> Format(IList<ValidationError> errors);
    }
}
