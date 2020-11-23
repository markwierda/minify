using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace mini_spotify.DAL
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
                           
            string prod = "Server=127.0.0.1; database=Spotify; Integrated Security=False; User id=sa; Password=PieterBrouwer01!; ConnectRetryCount=0; MultipleActiveResultSets=True";
            string local = "Server=(localdb)\\MSSQLLocalDB;Database=Spotify;Trusted_Connection=True;";// ConfigurationManager.ConnectionStrings["Dev"].ConnectionString;
            builder.UseSqlServer(prod, providerOptions => providerOptions.CommandTimeout(60))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            return new AppDbContext(builder.Options);
        }
    }
}
