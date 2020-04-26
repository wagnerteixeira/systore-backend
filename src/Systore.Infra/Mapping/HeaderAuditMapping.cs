using Microsoft.EntityFrameworkCore;
using Systore.Domain.Entities;

namespace Systore.Infra.Mapping
{
    public class HeaderAuditMapping : IEntityTypeConfiguration<HeaderAudit>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<HeaderAudit> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("headeraudit");

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.TableName)
                .HasMaxLength(50);

            builder.Property(p => p.Date)
                .HasColumnType("datetime");

            builder.Property(p => p.UserName)
                .HasMaxLength(30);

            builder.Property(p => p.Operation)
                .HasColumnType("TINYINT");
        }
    }
}
