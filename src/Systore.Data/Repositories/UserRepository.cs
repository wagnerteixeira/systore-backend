using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Systore.Data.Abstractions;
using Systore.Domain.Entities;
using Systore.Infra.Context;

namespace Systore.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(SystoreContext context, IHeaderAuditRepository headerAuditRepository) : base(context, headerAuditRepository)
        {

        }

        public async Task<User> GetUserByUsernameAndPassword(string userName, string password)
        {
            return await _entities.Where(c => c.UserName.ToUpper() == userName.ToUpper() && c.Password == password).FirstOrDefaultAsync();
        }
    }
}
