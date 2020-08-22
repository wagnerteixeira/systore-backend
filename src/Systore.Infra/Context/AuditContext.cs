using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Systore.Domain;
using Systore.Infra.Mapping;

namespace Systore.Infra.Context
{
    public class AuditContext : DbContext
    {
        private AppSettings _appSettings { get; set; }

        public AuditContext(
            DbContextOptions<AuditContext> options,
            IOptions<AppSettings> settings,
            IConfiguration configuration)
            : base(options)
        {
            _appSettings = settings.Value;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new HeaderAuditMapping());
            modelBuilder.ApplyConfiguration(new ItemAuditMapping());
        }
    }
}
