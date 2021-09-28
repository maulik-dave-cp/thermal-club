using ThermalClub.Modules.AdminUsers.Models;

namespace ThermalClub.Modules.AdminRolePermissions.Models
{
    public class AdminUsersAdminRoles
    {
        public int AdminRoleId { get; set; }
        public AdminRole AdminRole { get; set; }

        public int AdminUserId { get; set; }
        public AdminUser AdminUser { get; set; }
    }
}