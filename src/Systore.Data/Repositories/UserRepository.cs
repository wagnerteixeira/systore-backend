using Systore.Domain.Entities;
using Systore.Data.Abstractions;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Systore.Infra.Context;
using Microsoft.Extensions.Logging;

namespace Systore.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ISystoreContext context, IHeaderAuditRepository headerAuditRepository, ILogger logger) : base(context, headerAuditRepository, logger)
        {

        }

        public async Task<User> GetUserByUsernameAndPassword(string userName, string password)
        {
            return await _entities.Where(c => c.UserName.ToUpper() == userName.ToUpper() && c.Password == password).FirstOrDefaultAsync();
        }
    }
}
