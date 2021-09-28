using ThermalClub.Modules.AdminRolePermissions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ThermalClub.Modules.AdminRolePermissions.Data.Configurations
{
    public class AdminRolesAdminPermissionsConfiguration : IEntityTypeConfiguration<AdminRolesAdminPermissions>
    {
        public void Configure(EntityTypeBuilder<AdminRolesAdminPermissions> builder)
        {
	        builder.HasKey(t => new { t.AdminRoleId, t.AdminPermissionId });
        }
    }
}