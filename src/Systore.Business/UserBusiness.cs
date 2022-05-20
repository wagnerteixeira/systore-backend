using Systore.CrossCutting.Models;
using Systore.Repositories.Interfaces;

namespace Systore.Business;

public class UserBusiness : GenericCrudBusiness<User, int>
{
    public UserBusiness(IGenericCrudRepository<User, int> genericCrudRepository) : 
        base(genericCrudRepository)
    {
    }
}