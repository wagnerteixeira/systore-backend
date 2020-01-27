using System;
using System.Reflection;
using Systore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Systore.Domain;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace Systore.Infra.Context
{

    public partial class SystoreContext : DbContext, ISystoreContext
    {
        public DbContext Instance => this;

        private AppSettings _appSettings { get; set; }

        private readonly IConfiguration _configuration;

        public SystoreContext(
            DbContextOptions<SystoreContext> options,
            IOptions<AppSettings> settings,
            IConfiguration configuration)
            : base(options)
        {
            _appSettings = settings.Value;
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SystoreContext).Assembly, c => c.Name.Contains(_appSettings.DatabaseType) && !c.Name.Contains("Audit"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<BillReceive> BillReceives { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<ItemSale> ItemSales { get; set; }
    }
}
