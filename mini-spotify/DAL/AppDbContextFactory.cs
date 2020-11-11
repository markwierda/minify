using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace mini_spotify.DAL
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            string con = "Server=(localdb)\\MSSQLLocalDB;Database=Spotify;Trusted_Connection=True;";// ConfigurationManager.ConnectionStrings["Dev"].ConnectionString;
            builder.UseSqlServer(con, providerOptions => providerOptions.CommandTimeout(60))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            return new AppDbContext(builder.Options);
        }
    }
}
