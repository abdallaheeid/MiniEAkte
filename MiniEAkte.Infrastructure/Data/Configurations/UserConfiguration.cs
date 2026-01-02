using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniEAkte.Domain.Entities;
using MiniEAkte.Domain.Enums;

namespace MiniEAkte.Infra.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Username).IsRequired().HasMaxLength(100);
            builder.HasIndex(u => u.Username).IsUnique();
            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.Roles).IsRequired();

            // Roles will be stored as a simple string for now: I will refactor this later
            builder.Property(x => x.Roles)
                .HasConversion(
                    roles => string.Join(',', roles),
                    value => value.Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(r => Enum.Parse<UserRole>(r))
                        .ToList()
                );
        }
    }
}
