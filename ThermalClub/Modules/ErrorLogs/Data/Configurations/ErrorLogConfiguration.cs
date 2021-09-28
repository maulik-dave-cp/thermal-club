using ThermalClub.Modules.ErrorLogs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ThermalClub.Modules.ErrorLogs.Data.Configurations
{
    public class ErrorLogConfiguration : IEntityTypeConfiguration<ErrorLog>
    {
        public void Configure(EntityTypeBuilder<ErrorLog> builder)
        {
            builder.Property(v => v.ErrorType)
            .IsRequired()
            .HasMaxLength(50);
            builder.Property(v => v.Description)
            .IsRequired();
            builder.Property(v => v.Stacktrace)
            .IsRequired();
        }
    }
}