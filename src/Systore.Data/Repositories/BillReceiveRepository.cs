using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Systore.Data.Abstractions;
using Systore.Domain.Entities;
using Systore.Domain.Enums;
using Systore.Infra.Context;

namespace Systore.Data.Repositories
{
    public class BillReceiveRepository : BaseRepository<BillReceive>, IBillReceiveRepository
    {
        public BillReceiveRepository(SystoreContext context, IHeaderAuditRepository headerAuditRepository) : base(context, headerAuditRepository)
        {

        }

        public Task<int> CountBillReceivesByClient(int clientId) =>
          CountWhereAsync(c => c.ClientId == clientId);

        public async Task<List<BillReceive>> GetBillReceivesByClient(int ClientId)
        {
            var queryOpen = this._entities
               .Where(c => c.ClientId == ClientId && c.Situation == BillReceiveSituation.Open)
               .OrderBy(c => c.Code)
               .ThenBy(c => c.Quota);

            var queryClose = this._entities
              .Where(c => c.ClientId == ClientId && c.Situation == BillReceiveSituation.Closed)
              .OrderByDescending(c => c.Code)
              .ThenBy(c => c.Quota);

            var _billReceives = await queryOpen
              .Union(queryClose)
              .ToListAsync();

            return _billReceives;
        }

        public Task<List<BillReceive>> GetPaidBillReceivesByClient(int ClientId) =>
            this._entities
              .Where(c => c.ClientId == ClientId && c.Situation == BillReceiveSituation.Closed)
              .OrderBy(c => c.Code)
              .ThenBy(c => c.Quota)
              .ToListAsync();

        public Task<List<BillReceive>> GetNoPaidBillReceivesByClient(int ClientId) =>
            this._entities
              .Where(c => c.ClientId == ClientId && c.Situation == BillReceiveSituation.Open)
              .OrderBy(c => c.Code)
              .ThenBy(c => c.Quota)
              .ToListAsync();

        public async Task<int> NextCode()
        {
            return (await this._entities.MaxAsync(c => (int?)c.Code) ?? 0) + 1;
        }

        public async Task RemoveBillReceivesByCode(int Code)
        {
            this._entities.RemoveRange(this._entities.Where(c => c.Code == Code));
            await this.SaveChangesAsync();
        }

    }
}
