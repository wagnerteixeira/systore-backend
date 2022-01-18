
namespace Systore.Repositories;

public partial class UserRepository
{
    private const string GetUserByUsernameAndPasswordStatement =
        "select Id, Username, Password, Admin from user where UserName = @UserName and Password = @Password";
}