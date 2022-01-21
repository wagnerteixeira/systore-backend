using Dapper;
using Systore.CrossCutting.Models;
using Systore.Repositories.Interfaces;

namespace Systore.Repositories;

public partial class UserRepository : GenericCrudRepository<User, int>, IUserRepository
{
    public UserRepository(IDatabaseFactory databaseFactory)
        : base(
            databaseFactory,
            CreateSqlStatement,
            SelectSingleSqlStatement,
            SelectAllSqlStatement,
            DeleteSqlStatement,
            UpdateSqlStatement)
    {
    }

    public override object GetIdParam(int id)
    {
        return new {Id = id};
    }
    
    public override object GetIdParam(User user)
    {
        return new {user.Id};
    }

    public async Task<User?> GetUserByUsernameAndPassword(string userName, string password)
    {
        using var connection = DatabaseFactory.GetConnection();
        var user = await connection.QueryFirstOrDefaultAsync<User>(GetUserByUsernameAndPasswordStatement,
            new {UserName = userName, Password = password});
        connection.Close();
        return user;
    }
}