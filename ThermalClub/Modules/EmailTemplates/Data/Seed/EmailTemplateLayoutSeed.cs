using ThermalClub.Modules.Core.Data;
using ThermalClub.Modules.Core.Data.Seed;
using ThermalClub.Modules.EmailTemplates.Data.Emails;
using ThermalClub.Modules.EmailTemplates.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ThermalClub.Modules.EmailTemplates.Data.Seed
{
    public class EmailTemplateLayoutSeed : BaseSeed
    {
        private readonly SqlContext _context;
        public EmailTemplateLayoutSeed(SqlContext context) : base(context)
        {
            _context = context;
        }

        public override void Seed()
        {
            Layout();

            _context.SaveChanges();
        }

        private void Layout()
        {
            if (_context.Set<EmailTemplate>().Any(s => s.EmailTemplateType == EmailTemplateEmail.Layout))
                return;

            var content = ReadFile("EmailTemplates", "Layout.html");

            _context.Set<EmailTemplate>().Add(new EmailTemplate
            {
                Name = "Layout",
                EmailTemplateType = EmailTemplateEmail.Layout,
                Subject = "-",
                Content = content,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                TemplateType = TemplateType.User,
                ToEmails = new List<EmailClass>(),
                CcEmails = new List<EmailClass>(),
                BccEmails = new List<EmailClass>()
            });
        }
    }
}