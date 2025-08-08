using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace Edrs.ActionListenerService.Models
{
    [ExcludeFromCodeCoverage]
    public class RequestResponseLog
    {
        public Guid Id { get; set; } // Corresponds to the 'id' column (primary key)
        public string? Name { get; set; } // Corresponds to the 'name' column
        public XElement? RequestXmlLog { get; set; } // Corresponds to the 'request_xml_log' column
        public XElement? ResponseXmlLog { get; set; } // Corresponds to the 'response_xml_log' column
        public string? EdrsCaseId { get; set; } // Corresponds to the 'edrs_case_id' column
        public string? CreatedBy { get; set; } // Corresponds to the 'created_by' column
        public DateTime? CreatedDate { get; set; } // Corresponds to the 'created_date' column
        public string? UpdatedBy { get; set; } // Corresponds to the 'updated_by' column
        public DateTime? UpdatedDate { get; set; } // Corresponds to the 'updated_date' column
    }

}
