using Systore.Domain.Entities;
using Systore.Data.Abstractions;
using Systore.Infra.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Systore.Data.Repositories
{
    public class ItemSaleRepository : BaseRepository<ItemSale>, IItemSaleRepository
    {
        public ItemSaleRepository(ISystoreContext context, IHeaderAuditRepository headerAuditRepository, ILogger logger) : base(context, headerAuditRepository, logger)
        {

        }

        public Task<bool> ExistsByProduct(int productId)
        {
            return GetAll().Where(c => c.ProductId == productId).AnyAsync();
        }
    }
}
