using System.Collections.Generic;
using System.Threading.Tasks;
using Systore.Domain.Entities;

namespace Systore.Data.Abstractions
{
    public interface IBillReceiveRepository : IBaseRepository<BillReceive>
    {
        Task<List<BillReceive>> GetBillReceivesByClient(int ClientId);
        Task<List<BillReceive>> GetPaidBillReceivesByClient(int ClientId);
        Task<List<BillReceive>> GetNoPaidBillReceivesByClient(int ClientId);
        Task<int> CountBillReceivesByClient(int clientId);
        Task<int> NextCode();
        Task RemoveBillReceivesByCode(int Code);
    }
}
