namespace Systore.Repositories;

public record SqlStatements<T>(
    string CreateSqlStatement,
    string SelectSingleSqlStatement,
    string SelectAllSqlStatement,
    string DeleteSqlStatement,
    string UpdateSqlStatement,
    string GetUserByUsernameAndPasswordStatement);