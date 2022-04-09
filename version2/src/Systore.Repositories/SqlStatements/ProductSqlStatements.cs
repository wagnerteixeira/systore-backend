using Systore.CrossCutting.Models;

namespace Systore.Repositories;

public static class ProductSqlStatements
{
    public static string CreateSqlStatement =>
        "INSERT INTO product (" +
        "		SaleType," +
        "		Price," +
        "		ExpirationDays," +
        "		Description," +
        "		ExtraInformation," +
        "		PrintExpirationDate," +
        "		PrintDateOfPackaging," +
        "		ExportToBalance" +
        "	)" +
        " VALUES(" +
        "		@SaleType," +
        "		@Price," +
        "		@ExpirationDays," +
        "		@Description," +
        "		@ExtraInformation," +
        "		@PrintExpirationDate," +
        "		@PrintDateOfPackaging," +
        "		@ExportToBalance" +
        "	);";

    public static string SelectSingleSqlStatement =>
        "SELECT Id," +
        "	SaleType," +
        "	Price," +
        "	ExpirationDays," +
        "	Description," +
        "	ExtraInformation," +
        "	PrintExpirationDate," +
        "	PrintDateOfPackaging," +
        "	ExportToBalance" +
        " FROM product" +
        " WHERE Id = @Id;";

    public static string SelectAllSqlStatement =>
        "SELECT Id," +
        "	SaleType," +
        "	Price," +
        "	ExpirationDays," +
        "	Description," +
        "	ExtraInformation," +
        "	PrintExpirationDate," +
        "	PrintDateOfPackaging," +
        "	ExportToBalance" +
        " FROM product" +
        " WHERE 0=0"; // AND {0}";

    public static string DeleteSqlStatement =>
        "DELETE FROM product WHERE Id=@Id;";

    public static string UpdateSqlStatement =>
        "UPDATE product" +
        " SET SaleType = @SaleType," +
        "	Price = @Price," +
        "	ExpirationDays = @ExpirationDays," +
        "	Description = @Description," +
        "	ExtraInformation = @ExtraInformation," +
        "	PrintExpirationDate = @PrintExpirationDate," +
        "	PrintDateOfPackaging = @PrintDateOfPackaging," +
        "	ExportToBalance = @ExportToBalance" +
        " WHERE Id = @Id;";
}