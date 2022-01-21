using Systore.Business.Interfaces;
using Systore.CrossCutting.Models;
using Systore.Repositories.Interfaces;

namespace Systore.Business;

public class UserBusiness : IUserBusiness
{
    private readonly IUserRepository _userRepository;

    public UserBusiness(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Create(User user)
    {
        return await _userRepository.Create(user);
    }

    public async Task<bool> Delete(int id)
    {
        return await _userRepository.Delete(id);
    }

    public async Task<User> Update(User entity)
    {
        return await _userRepository.Update(entity);
    }

    public async Task<User?> Read(int id)
    {
        return await _userRepository.Read(id);
    }

    public async Task<IEnumerable<User>> Read()
    {
        return await _userRepository.Read();
    }
}