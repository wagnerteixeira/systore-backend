using System.Threading.Tasks;
using Systore.Data.Abstractions;
using Systore.Domain.Abstractions;
using Systore.Domain.Entities;

namespace Systore.Services
{
    public class ClientService : BaseService<Client>, IClientService
    {

        private readonly IBillReceiveRepository _billReceiveRepository;
        public ClientService(IClientRepository repository, IBillReceiveRepository billReceiveRepository) : base(repository)
        {
            _billReceiveRepository = billReceiveRepository;
        }

        public async Task<bool> ExistBillReceiveForClient(int ClientId)
        {
            var ret = await _billReceiveRepository.CountWhereAsync(c => c.ClientId == ClientId);
            return ret > 0;
        }
    }
}
