using System.Collections.Generic;

namespace ThermalClub.Modules.AdminRolePermissions.Models.DTOs
{
    public class AdminRoleEditDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> Permissions { get; set; }
    }
}