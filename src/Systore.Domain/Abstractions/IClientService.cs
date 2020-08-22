using System.Threading.Tasks;
using Systore.Domain.Entities;

namespace Systore.Domain.Abstractions
{
    public interface IClientService : IBaseService<Client>
    {
        Task<bool> ExistBillReceiveForClient(int ClientId);
    }
}
