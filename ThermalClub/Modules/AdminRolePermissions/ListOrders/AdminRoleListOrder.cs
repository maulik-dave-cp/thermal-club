using System.Linq;
using ThermalClub.Modules.AdminRolePermissions.Models;
using ThermalClub.Modules.Core.Filters;
using ThermalClub.Modules.Core.ListOrders;

namespace ThermalClub.Modules.AdminRolePermissions.ListOrders
{
    public class AdminRoleListOrder : BaseListOrder<AdminRole>
    {
        public AdminRoleListOrder(IQueryable<AdminRole> query, BaseFilterDto dto) : base(query, dto)
        {
        }

        internal void Name()
        {
            Query = OrderBy(t => t.Name);
        }
    }
}