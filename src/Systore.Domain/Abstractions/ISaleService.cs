using System.Threading.Tasks;
using Systore.Domain.Dtos;
using Systore.Domain.Entities;

namespace Systore.Domain.Abstractions
{
    public interface ISaleService : IBaseService<Sale>
    {
        Task<SaleDto> GetSaleFullById(int id);
        Task<string> UpdateAsync(SaleDto entity);

    }
}
