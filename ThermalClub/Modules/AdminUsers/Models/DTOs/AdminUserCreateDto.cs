using System.Collections.Generic;

namespace ThermalClub.Modules.AdminUsers.Models.DTOs
{
    public class AdminUserCreateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public List<int> Roles { get; set; }
    }
}