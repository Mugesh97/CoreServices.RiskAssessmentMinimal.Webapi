using System.Xml.Linq;

namespace Edrs.ActionListenerService.Models.Helpers.Interface
{
    public interface IXmlConverter
    {
        XElement ConvertObjectToXmlElement(object obj);
    }
}