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
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(e => e.Description)
                   .HasMaxLength(1000);

            builder.Property(e => e.ImageUrl)
                   .HasMaxLength(500);

            builder.Property(e => e.StartTime)
                   .IsRequired();

            builder.Property(e => e.EndTime)
                   .IsRequired();

            builder.Property(e => e.EventType)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.Status)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasDefaultValue("Scheduled");

            builder.Property(e => e.OwnerId)
                   .IsRequired();

            builder.HasOne(e => e.Owner)
                   .WithMany()
                   .HasForeignKey(e => e.OwnerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.CreatedDate)
                   .HasColumnType("datetime(6)")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            builder.Property(e => e.UpdatedDate);
        }
    }
}
