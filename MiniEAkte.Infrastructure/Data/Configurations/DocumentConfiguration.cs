using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniEAkte.Domain.Entities;

namespace MiniEAkte.Infra.Data.Configurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.DocumentType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Subject)
                .HasMaxLength(200);

            builder.Property(d => d.Tags)
                .HasMaxLength(500);

            builder.Property(d => d.FilePath)
                .HasMaxLength(500);

            builder.Property(d => d.CreatedAt)
                .IsRequired();

            // FK only (navigation can be added later)
            builder.HasIndex(d => d.CaseFileId);
        }
    }
}
