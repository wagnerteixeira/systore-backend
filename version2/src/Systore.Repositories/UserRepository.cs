using Dapper;
using Systore.CrossCutting.Models;
using Systore.Repositories.Interfaces;

namespace Systore.Repositories;

public partial class UserRepository : IUserRepository
{
    private readonly IDatabaseFactory _databaseFactory;
    public UserRepository(IDatabaseFactory databaseFactory)
    {
        _databaseFactory = databaseFactory;
    }
    
    public async Task<User?> GetUserByUsernameAndPassword(string userName, string password)
    {
        using var connection = _databaseFactory.GetConnection();
        var user = await connection.QueryFirstOrDefaultAsync<User>(GetUserByUsernameAndPasswordStatement, new {UserName = userName, Password = password});
        connection.Close();
        return user;
    }
}