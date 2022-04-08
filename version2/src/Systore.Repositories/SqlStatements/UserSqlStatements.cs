using Systore.CrossCutting.Models;

namespace Systore.Repositories;

public static class UserSqlStatements 
{
    public static string CreateSqlStatement =>
        "INSERT INTO user (UserName, Password, Admin) VALUES(@UserName, @Password, @Admin);";

    public static string SelectSingleSqlStatement =>
        "SELECT Id, UserName, Password, Admin FROM user WHERE Id = @Id;";

    public static string SelectAllSqlStatement =>
        "SELECT Id, UserName, Password, Admin FROM user WHERE 0=0"; // AND {0}";

    public static string DeleteSqlStatement =>
        "DELETE FROM user WHERE Id=@Id;";

    public static string UpdateSqlStatement =>
        "UPDATE user SET UserName=@UserName, Password=@Password, Admin=@Admin WHERE Id=@Id;";

    public static string GetUserByUsernameAndPasswordStatement =>
        "select Id, Username, Password, Admin from user where UserName = @UserName and Password = @Password;";
}