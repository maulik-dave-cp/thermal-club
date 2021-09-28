using ThermalClub.Modules.AdminRolePermissions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ThermalClub.Modules.AdminRolePermissions.Data.Configurations
{
    public class AdminPermissionConfiguration : IEntityTypeConfiguration<AdminPermission>
    {
        public void Configure(EntityTypeBuilder<AdminPermission> builder)
        {
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(t => t.Name)
	            .IsUnique();

            builder.Property(t => t.DisplayName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Ignore(t => t.Depth);
        }
    }
}