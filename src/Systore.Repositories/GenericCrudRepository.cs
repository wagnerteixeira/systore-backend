using Systore.Repositories.Interfaces;
using Dapper;

namespace Systore.Repositories;

public class GenericCrudRepository<Type, IdType> : IGenericCrudRepository<Type, IdType>
{
    protected readonly IDatabaseFactory? DatabaseFactory;
    private const string _getIdentitySql = "SELECT LAST_INSERT_ID() AS Id";
    private readonly string _createSqlStatement;
    private readonly string _selectSingleSqlStatement;
    private readonly string _selectAllSqlStatement;
    private readonly string _deleteSqlStatement;
    private readonly string _updateSqlStatement;


    public GenericCrudRepository(
        IDatabaseFactory? databaseFactory,
        SqlStatements<Type> sqlStatements)
    {
        DatabaseFactory = databaseFactory;
        _createSqlStatement = sqlStatements.CreateSqlStatement;
        _selectSingleSqlStatement = sqlStatements.SelectSingleSqlStatement;
        _selectAllSqlStatement = sqlStatements.SelectAllSqlStatement;
        _deleteSqlStatement = sqlStatements.DeleteSqlStatement;
        _updateSqlStatement = sqlStatements.UpdateSqlStatement;
    }

    public virtual object GetIdParam(IdType id)
    {
        return new {Id = id};
    }
    public async Task<Type> Create(Type entity)
    {
        using var connection = DatabaseFactory.GetConnection();
        var query = $"{_createSqlStatement}{_getIdentitySql}";
        var id = await connection.QueryFirstAsync<IdType>(query, entity);
        var param = GetIdParam(id);
        var resultEntity = await connection.QueryFirstAsync<Type>(_selectSingleSqlStatement, param);
        connection.Close();
        return resultEntity;
    }

    public async Task<bool> Delete(IdType id)
    {
        using var connection = DatabaseFactory.GetConnection();
        var param = GetIdParam(id);
        return (await connection.ExecuteAsync(_deleteSqlStatement, param)) == 1;
    }

    public async Task<Type> Update(IdType id, Type entity)
    {
        using var connection = DatabaseFactory.GetConnection();
        if ((await connection.ExecuteAsync(_updateSqlStatement, entity)) == 0)
        {
            throw new Exception("Update error");
        }

        var param = GetIdParam(id);
        var resultEntity = await connection.QueryFirstAsync<Type>(_selectSingleSqlStatement, param);
        connection.Close();
        return resultEntity;
    }

    public async Task<Type?> Read(IdType id)
    {
        using var connection = DatabaseFactory.GetConnection();
        var param = GetIdParam(id);
        var resultEntity = await connection.QueryFirstOrDefaultAsync<Type>(_selectSingleSqlStatement, param);
        connection.Close();
        return resultEntity;
    }

    public async Task<IEnumerable<Type>> Read()
    {
        using var connection = DatabaseFactory.GetConnection();
        var result = await connection.QueryAsync<Type>(_selectAllSqlStatement);
        connection.Close();
        return result;
    }
}