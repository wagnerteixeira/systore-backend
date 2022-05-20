using Dapper;
using Systore.CrossCutting.Models;
using Systore.Repositories.Interfaces;

namespace Systore.Repositories;

public partial class UserRepository : GenericCrudRepository<User, int>, IUserRepository
{
    public UserRepository(IDatabaseFactory? databaseFactory, SqlStatements<User> sqlStatements)
        : base(
            databaseFactory,
            sqlStatements)
    {
    }
    
    public override object GetIdParam(int id)
    {
        return new {Id = id};
    }
    public async Task<User?> GetUserByUsernameAndPassword(string userName, string password)
    {
        using var connection = DatabaseFactory.GetConnection();
        var user = await connection.QueryFirstOrDefaultAsync<User>(UserSqlStatements.GetUserByUsernameAndPasswordStatement,
            new {UserName = userName, Password = password});
        connection.Close();
        return user;
    }
}