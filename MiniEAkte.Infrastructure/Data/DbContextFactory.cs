using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MiniEAkte.Infra.Data
{
    public class DbContextFactory
    {
        public static AppDbContext CreateDbContext(string connectionString)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).Options;
            return new AppDbContext(options);
        }
    }
}
