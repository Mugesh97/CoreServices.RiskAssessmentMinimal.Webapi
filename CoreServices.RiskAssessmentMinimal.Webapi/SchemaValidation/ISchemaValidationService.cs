using Newtonsoft.Json.Schema;

namespace CoreServices.RiskAssessmentMinimal.Webapi.SchemaValidation
{
    public interface ISchemaValidationService
    {
        void ValidateSchema(IList<ValidationError> errors);
    }
}
