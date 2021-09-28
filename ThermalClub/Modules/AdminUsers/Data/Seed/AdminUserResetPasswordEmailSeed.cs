using ThermalClub.Modules.AdminUsers.Data.Emails;
using ThermalClub.Modules.Core.Data;
using ThermalClub.Modules.Core.Data.Seed;
using ThermalClub.Modules.EmailTemplates.Models;
using System;
using System.Linq;

namespace ThermalClub.Modules.AdminUsers.Data.Seed
{
    public class AdminUserResetPasswordEmailSeed : BaseSeed
    {
        private readonly SqlContext _context;

        public AdminUserResetPasswordEmailSeed(SqlContext context) : base(context)
        {
            _context = context;

            OrderId = 50;
        }

        public override void Seed()
        {
            if (_context.Set<EmailTemplate>().Any(s => s.EmailTemplateType == AdminUserEmail.AdminUserResetPassword))
                return;

            var content = ReadFile("AdminUsers", "AdminUserResetPassword.html");

            _context.Set<EmailTemplate>().Add(new EmailTemplate
            {
                Name = "Reset Password Admin",
                EmailTemplateType = AdminUserEmail.AdminUserResetPassword,
                TemplateType = TemplateType.User,
                // ToEmails = new List<EmailClass> { new EmailClass() { Email = "info@example.com" } },
                Subject = "Password Reset Request on {%WebsiteName%}",
                Content = content,
                HideEmailSection = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            });

            _context.SaveChanges();
        }
    }
}
