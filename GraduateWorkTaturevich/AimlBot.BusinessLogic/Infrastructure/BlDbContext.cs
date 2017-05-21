using System.Data.Entity;
using System.Diagnostics;
using BusinessLogic.Entities.Domains;
using BusinessLogic.Entities.FactoryDomain;
using BusinessLogic.Entities.Infrastructure;
using EventLog = BusinessLogic.Entities.Infrastructure.EventLog;

namespace BusinessLogic.Infrastructure
{
    public class BlDbContext : DbContext
    {
        public BlDbContext()
        {
            //Database.SetInitializer<BlDbContext>(null);
            Database.SetInitializer(new DropCreateDatabaseAlways<BlDbContext>());
            var test = Database.Connection.ConnectionString;
            Debug.WriteLine(test);
            Database.SetInitializer(new CreateDatabaseIfNotExists<BlDbContext>());
            Configuration.ProxyCreationEnabled = false;
        }

        public BlDbContext(string connectionString)
            : base(connectionString)
        {
            //Database.SetInitializer<BlDbContext>(null);
            //Database.SetInitializer(new DropCreateDatabaseAlways<BlDbContext>());
            var test = Database.Connection.ConnectionString;
            Debug.WriteLine(test);
            Database.SetInitializer(new CreateDatabaseIfNotExists<BlDbContext>());
            Database.Exists();
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Bot> Bots { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<EventLog> Eventlogs { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Password> Passwords { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Domain> Domains { get; set; }

        public DbSet<DomainArea> DomainAreas { get; set; }

        public DbSet<DomainAttributes> DomainAttributes { get; set; }
    }
}
