using System;
using System.Collections.Generic;
using System.Linq;
using ThermalClub.Modules.Core.Data;
using ThermalClub.Modules.EmailTemplates.Models;
using Z.EntityFramework.Plus;

namespace ThermalClub.Modules.EmailTemplates.Data.Repositories
{
    public interface IEmailTemplateRepository : IRepository<EmailTemplate>
    {
        IEnumerable<EmailTemplate> GetCached();
        EmailTemplate GetEmailTemplateByType(string emailTemplateType);
    }

    public class EmailTemplateRepository : Repository<EmailTemplate>, IEmailTemplateRepository
    {
        public EmailTemplateRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        public IEnumerable<EmailTemplate> GetCached()
        {
            return AsNoTracking.FromCache("EmailTemplates").ToList();
        }

        public EmailTemplate GetEmailTemplateByType(string emailTemplateType)
        {
            var emailTemplate = GetCached()
                .FirstOrDefault(w => w.EmailTemplateType == emailTemplateType);

            if (emailTemplate == null)
                throw new Exception($"Email template not found {emailTemplateType}");

            return emailTemplate;
        }
    }
}
