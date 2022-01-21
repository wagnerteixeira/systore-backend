using Systore.CrossCutting.Models;

namespace Systore.Business.Interfaces;

public interface IUserBusiness
{
    Task<User> Create(User user);
    Task<bool> Delete(int id);
    Task<User> Update(User entity);
    Task<User?> Read(int id);
    Task<IEnumerable<User>> Read();
}