using System.Collections.Generic;

namespace ThermalClub.Modules.AdminRolePermissions.Models.DTOs
{
    public class AdminRoleCreateDto
    {
        public string Name { get; set; }
        public List<int> Permissions { get; set; }
    }
}