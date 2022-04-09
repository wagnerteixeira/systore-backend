using Microsoft.AspNetCore.Authorization;
using Serilog;
using Systore.CrossCutting.Models;

namespace Systore.Api.Controllers;

/// <summary>
/// Product Controller
/// </summary>
[AllowAnonymous]
public class ProductController : GenericCrudController<Product, int>
{
    /// <summary>
    /// 
    /// </summary>
    public ProductController(ILogger logger) : base(logger)
    {
    }
}

