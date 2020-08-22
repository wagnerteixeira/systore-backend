using Systore.Data.Abstractions;
using Systore.Domain.Abstractions;
using Systore.Domain.Entities;

namespace Systore.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IUserRepository repository) : base(repository)
        {

        }
    }
}
