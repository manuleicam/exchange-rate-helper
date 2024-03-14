namespace Data.PostGreSql
{
    using Data.PostGreSql.Models;
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql("Host=localhost;Port=5432;Database=exchangeratesbd;Username=myuser;Password=mypassword;");
        }

        public DbSet<ExchangeRateSql> ExchangeRates { get; set; }
    }
}