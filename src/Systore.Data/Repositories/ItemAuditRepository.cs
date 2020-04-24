using Systore.Domain.Entities;
using Systore.Data.Abstractions;
using Systore.Infra.Context;

namespace Systore.Data.Repositories
{
    public class ItemAuditRepository : BaseRepository<ItemAudit>, IItemAuditRepository
    {
        public ItemAuditRepository(IAuditContext context, IHeaderAuditRepository headerAuditRepository) : base(context, headerAuditRepository)
        {

        }        
    }
}
