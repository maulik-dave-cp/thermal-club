using ThermalClub.Modules.AdminUsers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ThermalClub.Modules.AdminUsers.Data.Configurations
{
	public class AdminUserConfiguration : IEntityTypeConfiguration<AdminUser>
    {
        public void Configure(EntityTypeBuilder<AdminUser> builder)
        {
	        builder.Property(t => t.Name)
		        .IsRequired()
		        .HasMaxLength(100);

	        builder.Property(t => t.Email)
		        .IsRequired()
		        .HasMaxLength(100);

	        builder.HasIndex(t => t.Email).IsUnique();


			builder.Property(t => t.Password)
		        .IsRequired()
		        .HasMaxLength(256);

	        builder.Property(t => t.Salt)
		        .IsRequired()
		        .HasMaxLength(128);

	        builder.Property(t => t.ForgotPasswordToken)
		        .HasMaxLength(256);
        }
    }
}