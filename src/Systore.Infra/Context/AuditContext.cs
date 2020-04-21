using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Systore.Domain;

namespace Systore.Infra.Context
{
    public partial class AuditContext : DbContext, IAuditContext
    {
        public DbContext Instance => this;

        private AppSettings _appSettings { get; set; }
        
        public AuditContext(DbContextOptions<AuditContext> options, IOptions<AppSettings> settings, IConfiguration configuration)
            : base(options)
        {
            _appSettings = settings.Value;            
        }       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuditContext).Assembly, c => c.Name.Contains(_appSettings.DatabaseType) && c.Name.Contains("Audit"));
        }        
    }
}
