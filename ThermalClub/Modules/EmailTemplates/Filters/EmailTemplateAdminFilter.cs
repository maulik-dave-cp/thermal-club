using System.Linq;
using ThermalClub.Modules.Core.Filters;
using ThermalClub.Modules.EmailTemplates.Models;
using ThermalClub.Modules.EmailTemplates.Models.DTOs;

namespace ThermalClub.Modules.EmailTemplates.Filters
{
    public class EmailTemplateAdminFilter : BaseFilter<EmailTemplate, EmailTemplateAdminFilterDto>
    {
        public EmailTemplateAdminFilter(IQueryable<EmailTemplate> query, EmailTemplateAdminFilterDto dto) : base(query, dto) { }

        internal void Id()
        {
            Query = Query.Where(w => w.Id == Dto.Id);
        }

        internal void Name()
        {
            Query = Query.Where(w => w.Name.Contains(Dto.Name));
        }
    }
}
