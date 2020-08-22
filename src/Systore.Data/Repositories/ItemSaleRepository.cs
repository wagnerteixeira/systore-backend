using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Systore.Data.Abstractions;
using Systore.Domain.Entities;
using Systore.Infra.Context;

namespace Systore.Data.Repositories
{
    public class ItemSaleRepository : BaseRepository<ItemSale>, IItemSaleRepository
    {
        public ItemSaleRepository(SystoreContext context, IHeaderAuditRepository headerAuditRepository) : base(context, headerAuditRepository)
        {

        }

        public Task<bool> ExistsByProduct(int productId)
        {
            return GetAll().Where(c => c.ProductId == productId).AnyAsync();
        }
    }
}
