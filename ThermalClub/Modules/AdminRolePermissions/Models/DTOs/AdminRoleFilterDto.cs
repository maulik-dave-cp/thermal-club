using ThermalClub.Modules.Core.Filters;

namespace ThermalClub.Modules.AdminRolePermissions.Models.DTOs
{
    public class AdminRoleFilterDto : BaseFilterDto
    {
        public string Name { get; set; }

        public AdminRoleFilterDto()
        {
            SortColumn = "Name";
            SortType = "ASC";
        }
    }
}