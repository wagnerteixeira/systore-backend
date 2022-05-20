using Microsoft.AspNetCore.Authorization;
using Serilog;
using Systore.CrossCutting.Models;

namespace Systore.Api.Controllers;

/// <summary>
/// Client Controller
/// </summary>
[AllowAnonymous]
public class ClientController : GenericCrudController<Client, int>
{
    /// <summary>
    /// 
    /// </summary>
    public ClientController(ILogger logger) : base(logger)
    {
    }
}