
namespace Systore.Repositories;

public partial class UserRepository
{
    private const string CreateSqlStatement =
        "INSERT INTO user (UserName, Password, Admin) VALUES(@UserName, @Password, @Admin);";
    private const string SelectSingleSqlStatement =
        "SELECT Id, UserName, Password, Admin FROM user WHERE Id = @Id;";
    private const string SelectAllSqlStatement =
        "SELECT Id, UserName, Password, Admin FROM user WHERE 0=0 AND {0}";
    private const string DeleteSqlStatement =
        "DELETE FROM user WHERE Id=@Id;";
    private const string UpdateSqlStatement = 
        "UPDATE user SET UserName=@UserName, Password=@Password, Admin=@Admin WHERE Id=@Id;";
    private const string GetUserByUsernameAndPasswordStatement =
        "select Id, Username, Password, Admin from user where UserName = @UserName and Password = @Password;";
}