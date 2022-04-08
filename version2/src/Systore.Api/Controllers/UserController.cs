using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Systore.Business.Interfaces;
using Systore.CrossCutting.Models;
using ILogger = Serilog.ILogger;

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