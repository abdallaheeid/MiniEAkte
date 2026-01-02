using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniEAkte.Domain.Entities;

namespace MiniEAkte.Infra.Data.Configurations
{
    public class CaseConfiguration : IEntityTypeConfiguration<CaseFile>
    {
        public void Configure(EntityTypeBuilder<CaseFile> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.FileNumber).IsRequired().HasMaxLength(50);
            builder.HasIndex(c => c.FileNumber).IsUnique();
            builder.Property(c => c.Title).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Status).IsRequired();
            builder.Property(c => c.Owner).IsRequired(false).HasMaxLength(100);
            builder.Property(c => c.CreatedAt).IsRequired();
            builder.Property(c => c.UpdatedAt).IsRequired();
            builder.Property(c => c.ClosedAt).IsRequired(false);
        }
    }
}
