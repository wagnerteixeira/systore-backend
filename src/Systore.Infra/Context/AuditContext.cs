using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Systore.Domain;
using Systore.Domain.Entities;

namespace Systore.Infra.Context
{
   

    public partial class AuditContext : DbContext, IAuditContext
    {
        public DbContext Instance => this;

        private AppSettings _appSettings { get; set; }

        private readonly IConfiguration _configuration;

        public AuditContext(DbContextOptions<AuditContext> options, IOptions<AppSettings> settings, IConfiguration configuration)
            : base(options)
        {
            _appSettings = settings.Value;
            _configuration = configuration;
        }       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuditContext).Assembly, c => c.Name.Contains(_appSettings.DatabaseType) && c.Name.Contains("Audit"));
        }

        public DbSet<HeaderAudit> HeaderAudits { get; set; }
        public DbSet<ItemAudit> ItemAudits { get; set; }
    }
}
