using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MiniEAkte.Infra.Data
{
    public class AppDbContextDesignTimeFactory
        : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var connectionString =
                "Server=localhost;Port=3306;Database=minieakte;User=root;Password=12345678;";

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)
            );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}