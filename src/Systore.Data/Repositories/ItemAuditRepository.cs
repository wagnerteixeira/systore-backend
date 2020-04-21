using Systore.Domain.Entities;
using Systore.Data.Abstractions;
using Systore.Infra.Context;
using Microsoft.Extensions.Logging;

namespace Systore.Data.Repositories
{
    public class ItemAuditRepository : BaseRepository<ItemAudit>, IItemAuditRepository
    {
        public ItemAuditRepository(IAuditContext context, IHeaderAuditRepository headerAuditRepository, ILogger logger) : base(context, headerAuditRepository, logger)
        {

        }        
    }
}
