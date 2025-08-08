using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Core.Services.Libraries.Base.Lgs;
using Newtonsoft.Json.Schema;

namespace CoreServices.RiskAssessmentMinimal.Webapi.SchemaValidation
{
    public class ValidationErrorsFormatter : IValidationErrorsFormatter
    {
        public List<LgsMessageDto> Format(IList<ValidationError> errors)
        {
            var messages = new List<LgsMessageDto>();

            foreach (var v in errors)
            {
                var property = string.Empty;

                if (string.IsNullOrEmpty(v.Path) && v.Value != null)
                {
                    property = string.Join(',', ((IEnumerable)v.Value).Cast<object>().Select(x => x.ToString()).ToArray());
                }
                else
                {
                    property = v.Path;
                }

                if (!string.IsNullOrEmpty(property))
                    messages.Add(new LgsMessageDto(property, v.Message));

                foreach (var vchild in v.ChildErrors)
                {
                    if (string.IsNullOrEmpty(vchild.Path) && vchild.Value != null)
                    {
                        property = string.Join(',', ((IEnumerable)vchild.Value).Cast<object>().Select(x => x.ToString()).ToArray());
                    }
                    else
                    {
                        property = vchild.Path;
                    }
                    if (!string.IsNullOrEmpty(property))
                        messages.Add(new LgsMessageDto(property, vchild.Message));
                }
            }

            return messages;
        }
    }
}
