using Edrs.ActionListenerService.Models.Helpers.Interface;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace Edrs.ActionListenerService.Models.Helpers
{
    [ExcludeFromCodeCoverage]
    public class XmlConverter : IXmlConverter
    {
        public XElement ConvertObjectToXmlElement(object obj)
        {
            return LoggingHelper.ConvertObjectToXmlElement(obj);
        }
    }
}
