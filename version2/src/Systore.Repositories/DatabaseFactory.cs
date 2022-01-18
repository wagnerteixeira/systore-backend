using System.Data;
using System.Data.Common;
using Systore.CrossCutting;
using Systore.Repositories.Interfaces;

namespace Systore.Repositories;


public class DatabaseFactory : IDatabaseFactory
{
    private readonly ApplicationConfig _config;
    private readonly DbProviderFactory _factory;

    public DatabaseFactory(ApplicationConfig config, DbProviderFactory factory)
    {
        _config = config;
        _factory = factory;
    }

    public IDbConnection GetConnection()
    {
        var connection = _factory.CreateConnection();
        connection.ConnectionString = _config.ConnectionStrings.DefaultConnection;

        return connection;
    }
}