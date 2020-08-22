using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Systore.Domain;
using Systore.Domain.Entities;
using Systore.Infra.Mapping;

namespace Systore.Infra.Context
{

    public class SystoreContext : DbContext
    {
        private AppSettings _appSettings { get; set; }

        public SystoreContext(
            DbContextOptions<SystoreContext> options,
            IOptions<AppSettings> settings,
            IConfiguration configuration)
            : base(options)
        {
            _appSettings = settings.Value;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BillReceiveMapping());
            modelBuilder.ApplyConfiguration(new ClientMapping());
            modelBuilder.ApplyConfiguration(new ItemSaleMapping());
            modelBuilder.ApplyConfiguration(new ProductMapping());
            modelBuilder.ApplyConfiguration(new SaleMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<BillReceive> BillReceives { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<ItemSale> ItemSales { get; set; }
    }
}
