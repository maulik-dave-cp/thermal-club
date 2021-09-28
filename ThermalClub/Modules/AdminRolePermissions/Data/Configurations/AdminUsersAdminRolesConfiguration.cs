using ThermalClub.Modules.AdminRolePermissions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ThermalClub.Modules.AdminRolePermissions.Data.Configurations
{
    public class AdminUsersAdminRolesConfiguration : IEntityTypeConfiguration<AdminUsersAdminRoles>
    {
        public void Configure(EntityTypeBuilder<AdminUsersAdminRoles> builder)
        {
	        builder.HasKey(t => new { t.AdminUserId, t.AdminRoleId });
        }
    }
}