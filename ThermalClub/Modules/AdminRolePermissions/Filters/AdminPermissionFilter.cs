using System.Linq;
using ThermalClub.Modules.AdminRolePermissions.Models;
using ThermalClub.Modules.AdminRolePermissions.Models.DTOs;
using ThermalClub.Modules.Core.Filters;

namespace ThermalClub.Modules.AdminRolePermissions.Filters
{
    public class AdminPermissionFilter : BaseFilter<AdminPermission, AdminPermissionFilterDto>
    {
        public AdminPermissionFilter(IQueryable<AdminPermission> query, AdminPermissionFilterDto dto) : base(query, dto)
        {
        }

        internal void Name()
        {
            Query = Query.Where(w => w.Name.Contains(Dto.Name));
        }

        internal void DisplayName()
        {
            Query = Query.Where(w => w.DisplayName.Contains(Dto.DisplayName));
        }
    }
}