using System;
using System.Collections.Generic;
using ThermalClub.Modules.Core.Data;
using Newtonsoft.Json;

namespace ThermalClub.Modules.EmailTemplates.Models
{
    public class EmailTemplate : ITrackable
    {
        public int Id { get; set; }

        public string EmailTemplateType { get; set; }

        public string Name { get; set; }

        public string FromName { get; set; }
        public string FromEmail { get; set; }

        public bool HideEmailSection { get; set; }
        internal string _toEmails { get; set; }

        public List<EmailClass> ToEmails
        {
            get => _toEmails == null ? null : JsonConvert.DeserializeObject<List<EmailClass>>(_toEmails);
            set => _toEmails = JsonConvert.SerializeObject(value);
        }

        internal string _bccEmails { get; set; }

        public List<EmailClass> BccEmails
        {
            get => _bccEmails == null ? null : JsonConvert.DeserializeObject<List<EmailClass>>(_bccEmails);
            set => _bccEmails = JsonConvert.SerializeObject(value);
        }

        internal string _ccEmails { get; set; }

        public List<EmailClass> CcEmails
        {
            get => _ccEmails == null ? null : JsonConvert.DeserializeObject<List<EmailClass>>(_ccEmails);
            set => _ccEmails = JsonConvert.SerializeObject(value);
        }

        public string Subject { get; set; }
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public TemplateType? TemplateType { get; set; }
    }

    public class EmailClass
    {
        public string Email { get; set; }
    }
}
