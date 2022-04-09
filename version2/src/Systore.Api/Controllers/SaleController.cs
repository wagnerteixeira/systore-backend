using Microsoft.AspNetCore.Authorization;
using Serilog;
using Systore.CrossCutting.Models;

namespace Systore.Api.Controllers;

/// <summary>
/// Sale Controller
/// </summary>
[AllowAnonymous]
public class SaleController : GenericCrudController<Sale, int>
{
    /// <summary>
    /// 
    /// </summary>
    public SaleController(ILogger logger) : base(logger)
    {
    }
}