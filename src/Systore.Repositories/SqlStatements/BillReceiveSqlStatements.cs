using Systore.CrossCutting.Models;

namespace Systore.Repositories;

public static class BillReceiveSqlStatements
{
    public static string CreateSqlStatement =>
        "INSERT INTO billreceive (" +
        "		ClientId," +
        "		Code," +
        "		Quota," +
        "		OriginalValue," +
        "		Interest," +
        "		FinalValue," +
        "		PurchaseDate," +
        "		DueDate," +
        "		PayDate," +
        "		DaysDelay," +
        "		Situation," +
        "		Vendor" +
        "	)" +
        " VALUES(" +
        "		@ClientId," +
        "		@Code," +
        "		@Quota," +
        "		@OriginalValue," +
        "		@Interest," +
        "		@FinalValue," +
        "		@PurchaseDate," +
        "		@DueDate," +
        "		@PayDate," +
        "		@DaysDelay," +
        "		@Situation," +
        "		@Vendor" +
        "	);";

    public static string SelectSingleSqlStatement =>
        "SELECT Id," +
        "	ClientId," +
        "	Code," +
        "	Quota," +
        "	OriginalValue," +
        "	Interest," +
        "	FinalValue," +
        "	PurchaseDate," +
        "	DueDate," +
        "	PayDate," +
        "	DaysDelay," +
        "	Situation," +
        "	Vendor" +
        " FROM billreceive;" +
        " WHERE Id = @Id;";

    public static string SelectAllSqlStatement =>
        "SELECT Id," +
        "	ClientId," +
        "	Code," +
        "	Quota," +
        "	OriginalValue," +
        "	Interest," +
        "	FinalValue," +
        "	PurchaseDate," +
        "	DueDate," +
        "	PayDate," +
        "	DaysDelay," +
        "	Situation," +
        "	Vendor" +
        " FROM billreceive;" +
        " WHERE 0=0"; // AND {0}";

    public static string DeleteSqlStatement =>
        "DELETE FROM billreceive WHERE Id=@Id;";

    public static string UpdateSqlStatement =>
        "UPDATE billreceive" +
        " SET ClientId = @ClientId," +
        "	Code = @Code," +
        "	Quota = @Quota," +
        "	OriginalValue = @OriginalValue," +
        "	Interest = @Interest," +
        "	FinalValue = @FinalValue," +
        "	PurchaseDate = @PurchaseDate," +
        "	DueDate = @DueDate," +
        "	PayDate = @PayDate," +
        "	DaysDelay = @DaysDelay," +
        "	Situation = @Situation," +
        "	Vendor = @Vendor" +
        " WHERE Id = @Id;";
}