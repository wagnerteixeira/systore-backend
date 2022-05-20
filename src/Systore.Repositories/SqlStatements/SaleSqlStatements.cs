using Systore.CrossCutting.Models;

namespace Systore.Repositories;

public static class SaleSqlStatements
{
    public static string CreateSqlStatement =>
        "INSERT INTO sale (ClientId, FinalValue, SaleDate, Vendor)" +
        " VALUES(@ClientId, @FinalValue, @SaleDate, @Vendor);";

    public static string SelectSingleSqlStatement =>
        "SELECT Id, " +
        "	ClientId, " +
        "	FinalValue, " +
        "	SaleDate, " +
        "	Vendor " +
        " FROM sale " +
        " WHERE Id = @Id;";

    public static string SelectAllSqlStatement =>
        "SELECT Id, " +
        "	ClientId, " +
        "	FinalValue, " +
        "	SaleDate, " +
        "	Vendor " +
        " FROM sale " +
        " WHERE 0=0"; // AND {0}";

    public static string DeleteSqlStatement =>
        "DELETE FROM sale WHERE Id=@Id;";

    public static string UpdateSqlStatement =>
        "UPDATE sale " +
        " SET ClientId = @ClientId, " +
        "	FinalValue = @FinalValue, " +
        "	SaleDate = @SaleDate, " +
        "	Vendor = @Vendor " +
        " WHERE Id = @Id; ";
}