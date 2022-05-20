using Microsoft.AspNetCore.Authorization;
using Serilog;
using Systore.CrossCutting.Models;

namespace Systore.Api.Controllers;

/// <summary>
/// User Controller
/// </summary>
[AllowAnonymous]
public class UserController : GenericCrudController<User, int>
{
    /// <summary>
    /// 
    /// </summary>
    public UserController(ILogger logger) : base(logger)
    {
    }
}