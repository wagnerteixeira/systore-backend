using Microsoft.AspNetCore.Authorization;
using Serilog;
using Systore.CrossCutting.Models;

namespace Systore.Api.Controllers;

/// <summary>
/// BillReceive Controller
/// </summary>
[AllowAnonymous]
public class BillReceiveController : GenericCrudController<BillReceive, int>
{
    /// <summary>
    /// 
    /// </summary>
    public BillReceiveController(ILogger logger) : base(logger)
    {
    }
}