using Systore.CrossCutting.Models;

namespace Systore.Repositories.Interfaces;

public interface IUserRepository 
{
    Task<User?> GetUserByUsernameAndPassword(string userName, string password);
}