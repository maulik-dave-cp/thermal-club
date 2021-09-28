using System.Collections.Generic;

namespace ThermalClub.Modules.EmailTemplates.Models.DTOs
{
    public class EmailTemplateEditAdminDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string CcEmail { get; set; }
        public string BccEmail { get; set; }
        public List<EmailClass> ToEmails { get; set; }
        public List<EmailClass> BccEmails { get; set; }
        public List<EmailClass> CcEmails { get; set; }
        public TemplateType TemplateType { get; set; }
    }
}
