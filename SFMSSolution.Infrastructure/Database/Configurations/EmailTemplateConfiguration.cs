using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SFMSSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Database.Configurations
{
    public class EmailTemplateConfiguration : IEntityTypeConfiguration<EmailTemplate>
    {
        public void Configure(EntityTypeBuilder<EmailTemplate> builder)
        {
            builder.ToTable("Emails");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.TemplateName).IsRequired();
            builder.Property(e => e.Subject).IsRequired();
            builder.Property(e => e.Body).IsRequired();

            // === BaseEntity fields ===
            builder.Property(b => b.CreatedDate)
                   .HasColumnType("datetime(6)")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            builder.Property(b => b.CreatedBy)
                   .IsRequired(false);

            builder.Property(b => b.UpdatedDate)
                   .HasColumnType("datetime(6)")
                   .IsRequired(false);

            builder.Property(b => b.UpdatedBy)
                   .IsRequired(false);
        }
    }
}
