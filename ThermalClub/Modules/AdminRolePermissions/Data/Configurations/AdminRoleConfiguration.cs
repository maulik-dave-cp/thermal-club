using ThermalClub.Modules.AdminRolePermissions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ThermalClub.Modules.AdminRolePermissions.Data.Configurations
{
	public class AdminRoleConfiguration : IEntityTypeConfiguration<AdminRole> 
    {
        public void Configure(EntityTypeBuilder<AdminRole> builder)
        {
	        builder.Property(t => t.Name)
		        .IsRequired()
		        .HasMaxLength(50);

	        builder.Property(t => t.SystemName)
		        .IsRequired()
		        .HasMaxLength(50);

	        builder.HasIndex(t => t.SystemName)
		        .IsUnique();
        }
    }
}