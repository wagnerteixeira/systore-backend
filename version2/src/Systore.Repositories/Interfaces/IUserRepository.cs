using Systore.CrossCutting.Models;

namespace Systore.Repositories.Interfaces;

public interface IUserRepository: IGenericCrudRepository<User, int>
{
    Task<User?> GetUserByUsernameAndPassword(string userName, string password);
}