using System;
using System.Collections.Generic;
using System.Text;
using MiniEAkte.Domain.Entities;
using MiniEAkte.Domain.Enums;
using MiniEAkte.Infra.Data;
using MiniEAkte.Infra.Identity;

namespace MiniEAkte.Infrastructure.Data.Seeds
{
    public static class AdminSeed
    {
        public static void Seed(AppDbContext db)
        {
            if (db.Users.Any())
            {
                return;
            }

            var adminUser = new User
            {
                Username = "admin",
                Roles = new List<UserRole> { UserRole.Admin }
            };

            var hasher = new PasswordHasher();
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "admin123");

            db.Users.Add(adminUser);
            db.SaveChanges();
        }
    }
}
