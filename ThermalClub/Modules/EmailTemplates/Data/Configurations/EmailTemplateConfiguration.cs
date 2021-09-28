using ThermalClub.Modules.EmailTemplates.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ThermalClub.Modules.EmailTemplates.Data.Configurations
{
	public class EmailTemplateConfiguration : IEntityTypeConfiguration<EmailTemplate>
    {
        public void Configure(EntityTypeBuilder<EmailTemplate> builder)
        {
	        builder.Property(t => t.Name)
		        .IsRequired()
		        .HasMaxLength(100);

	        builder.Property(t => t.EmailTemplateType)
		        .IsRequired()
		        .HasMaxLength(100);

	        builder.Property(t => t.FromName)
		        .HasMaxLength(100);

	        builder.Property(t => t.FromEmail)
		        .HasMaxLength(256);

	        builder.Ignore(t => t.ToEmails);
	        builder.Property(t => t._toEmails)
		        .HasColumnName("ToEmails");

	        builder.Ignore(t => t.BccEmails);
	        builder.Property(t => t._bccEmails)
		        .HasColumnName("BccEmails");

	        builder.Ignore(t => t.CcEmails);
	        builder.Property(t => t._ccEmails)
		        .HasColumnName("CcEmails");

	        builder.Property(t => t.Subject)
		        .IsRequired()
		        .HasMaxLength(100);

	        builder.Property(t => t.Content)
		        .IsRequired();
        }
    }
}