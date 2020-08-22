using Systore.Data.Abstractions;
using Systore.Domain.Entities;
using Systore.Infra.Context;

namespace Systore.Data.Repositories
{
    public class ItemAuditRepository : BaseRepository<ItemAudit>, IItemAuditRepository
    {
        public ItemAuditRepository(
            AuditContext context,
            IHeaderAuditRepository headerAuditRepository) : base(context, headerAuditRepository)
        {

        }
    }
}
