using System.Data;

namespace Systore.Repositories.Interfaces;

public interface IDatabaseFactory
{
    IDbConnection GetConnection();
}