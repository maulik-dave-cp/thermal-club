using ThermalClub.Modules.Core.Filters;

namespace ThermalClub.Modules.AdminRolePermissions.Models.DTOs
{
    public class AdminPermissionFilterDto : BaseFilterDto
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}