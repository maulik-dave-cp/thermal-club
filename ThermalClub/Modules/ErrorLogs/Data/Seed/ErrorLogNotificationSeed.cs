using ThermalClub.Modules.Core.Data;
using ThermalClub.Modules.Core.Data.Seed;
using ThermalClub.Modules.EmailTemplates.Models;
using ThermalClub.Modules.ErrorLogs.Data.Emails;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ThermalClub.Modules.ErrorLogs.Data.Seed
{
    public class ErrorLogNotificationSeed : BaseSeed
    {
        private readonly SqlContext _context;

        public ErrorLogNotificationSeed(SqlContext context) : base(context)
        {
            _context = context;
            OrderId = 70;
        }

        public override void Seed()
        {
            if (_context.Set<EmailTemplate>().Any(s => s.EmailTemplateType == ErrorLogEmail.ErrorLogNotificationEmail))
                return;

            var content = ReadFile("ErrorLogs", "ErrorLogNotification.html");

            _context.Set<EmailTemplate>().Add(new EmailTemplate
            {
                Name = "ErrorLog Notification",
                EmailTemplateType = ErrorLogEmail.ErrorLogNotificationEmail,
                TemplateType = TemplateType.Admin,
                FromEmail = "towersvc@towerenergy.com",
                FromName = "towersvc",
                ToEmails = new List<EmailClass>
                {
                    new EmailClass() { Email = "maulik.dave@commercepundit.com" },
                    new EmailClass() { Email = "manish.jha@commercepundit.com" }
                },
                Subject = "Error-Log",
                Content = content,
                HideEmailSection = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            });

            _context.SaveChanges();
        }
    }
}