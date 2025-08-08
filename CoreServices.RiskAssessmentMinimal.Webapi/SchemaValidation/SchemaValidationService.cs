using Core.Services.Libraries.Base.Common;
using Core.Services.Libraries.Base.Exceptions;
using Core.Services.Libraries.Base.Lgs;
using Newtonsoft.Json.Schema;

namespace CoreServices.RiskAssessmentMinimal.Webapi.SchemaValidation
{
    public class SchemaValidationService : ISchemaValidationService
    {
        private const string JsonConversionErrorMessage = "Error converting request to json";
        private const string ContentTypeNoRequestBody = "requestBody not found in schema";
        private readonly IValidationErrorsFormatter _validationErrorsFormatter;
        private readonly ILogger<SchemaValidationService> _logger;

        public SchemaValidationService(ILogger<SchemaValidationService> logger, IValidationErrorsFormatter validationErrorsFormatter)
        {
            _logger = logger;
            _validationErrorsFormatter = validationErrorsFormatter;
        }

        public void ValidateSchema(IList<ValidationError> errors)
        {
            ArgumentNullException.ThrowIfNull(errors);

            if (errors.Count == 0) return;

            try
            {
                var messages = _validationErrorsFormatter.Format(errors);
                throw CreateBadRequestException("Request body failed schema validation", messages);
            }
            catch (BadRequestException badRequestEx)
            {
                _logger.LogError(badRequestEx, "Schema validation failed.");
                throw;
            }
            catch (Exception ex)
            {
                HandleValidationException(ex);
            }
        }

        protected virtual void HandleValidationException(Exception ex)
        {
            if (ex.Message.Contains(JsonConversionErrorMessage))
            {
                throw CreateBadRequestException("Unable to convert request body to JSON",
                    [new LgsMessageDto(null, "Unable to convert request body to JSON")]);
            }

            if (ex.Message.Contains(ContentTypeNoRequestBody))
            {
                throw CreateBadRequestException("Content-Type application/json - request body not found",
                    [new LgsMessageDto(null, "'Content-Type' header supplied but no content found.")]);
            }

            throw new ApiApplicationException($"Failed to validate request against schema: {ex.Message}", ex);
        }

        private static BadRequestException CreateBadRequestException(string message, List<LgsMessageDto> messages)
        {
            return new BadRequestException(message)
            {
                Code = Constants.ErrorCodeValidation,
                Messages = messages
            };
        }

    }
}
