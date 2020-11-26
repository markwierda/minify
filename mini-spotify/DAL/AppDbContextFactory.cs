using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace mini_spotify.DAL
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args = null)
        {
            var configbuilder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            IConfigurationRoot configuration = configbuilder.Build();

            string connectionStringName = configuration.GetSection("Environment")["IsDevelopment"] switch
            {
                "false" => "Spotify-Database-prod",
                _ => "Spotify-Database-local"
            };

            string connectionString = configuration.GetConnectionString(connectionStringName);

            var builder = new DbContextOptionsBuilder<AppDbContext>();

            builder.UseSqlServer(connectionString, providerOptions => providerOptions.CommandTimeout(60));

            return new AppDbContext(builder.Options);
        }
    }
}
