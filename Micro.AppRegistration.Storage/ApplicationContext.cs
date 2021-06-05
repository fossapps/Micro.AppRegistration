using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Micro.AppRegistration.Storage
{
    public class ApplicationContext : DbContext
    {
        private readonly DatabaseConfig _db;
        public DbSet<Application> Applications { set; get; }

        public ApplicationContext(DbContextOptions options, IOptions<DatabaseConfig> dbOption) : base(options)
        {
            _db = dbOption.Value;
        }

        protected ApplicationContext(IOptions<DatabaseConfig> dbOption)
        {
            _db = dbOption.Value;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = new NpgsqlConnectionStringBuilder
            {
                Host = _db.Host,
                Port = _db.Port,
                Database = _db.Name,
                Username = _db.User,
                Password = _db.Password,
                SslMode = SslMode.Disable
            };
            optionsBuilder.UseNpgsql(connection.ConnectionString, x =>
            {
                x.MigrationsAssembly("Micro.AppRegistration.Storage");
            });
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
