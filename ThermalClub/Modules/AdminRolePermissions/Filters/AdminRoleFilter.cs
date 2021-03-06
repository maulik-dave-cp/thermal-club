using System.Linq;
using ThermalClub.Modules.AdminRolePermissions.Models;
using ThermalClub.Modules.AdminRolePermissions.Models.DTOs;
using ThermalClub.Modules.Core.Filters;

namespace ThermalClub.Modules.AdminRolePermissions.Filters
{
    public class AdminRoleFilter : BaseFilter<AdminRole, AdminRoleFilterDto>
    {
        public AdminRoleFilter(IQueryable<AdminRole> query, AdminRoleFilterDto dto) : base(query, dto)
        {
        }

        internal void Name()
        {
            Query = Query.Where(w => w.Name.Contains(Dto.Name));
        }
    }
}