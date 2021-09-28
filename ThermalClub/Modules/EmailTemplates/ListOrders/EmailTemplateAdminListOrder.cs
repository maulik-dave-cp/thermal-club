using System.Linq;
using ThermalClub.Modules.Core.Filters;
using ThermalClub.Modules.Core.ListOrders;
using ThermalClub.Modules.EmailTemplates.Models;

namespace ThermalClub.Modules.EmailTemplates.ListOrders
{
    public class EmailTemplateAdminListOrder : BaseListOrder<EmailTemplate>
    {
        public EmailTemplateAdminListOrder(IQueryable<EmailTemplate> query, BaseFilterDto dto) :
            base(query, dto)
        { }
        internal void Id()
        {
            Query = OrderBy(t => t.Id);
        }
        internal void Name()
        {
            Query = OrderBy(t => t.Name);
        }

        internal void CreatedAt()
        {
            Query = OrderBy(t => t.CreatedAt);
        }

        internal void UpdatedAt()
        {
            Query = OrderBy(t => t.UpdatedAt);
        }
    }
}
