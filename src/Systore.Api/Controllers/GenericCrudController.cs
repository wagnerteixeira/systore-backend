using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Systore.Business.Interfaces;
using ILogger = Serilog.ILogger;

namespace Systore.Api.Controllers;

/// <summary>
/// Type Controller
/// <see cref="GenericCrudController{Type, IdType}"/> for more information.
/// </summary>
[ApiController]
[Route("[controller]")]
public abstract class GenericCrudController<Type, IdType> : ControllerBase
{
    private readonly ILogger _logger;
    
    /// <summary> 
    /// </summary>
    /// <param name="logger"></param>
    protected GenericCrudController(ILogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Create route
    /// </summary>
    /// <param name="crudBusiness"></param>
    /// <param name="entity"></param>
    /// <returns>Type</returns>
    [HttpPost("")]
    [Authorize]
    public async Task<IActionResult> Create([FromServices] IGenericCrudBusiness<Type, IdType> crudBusiness, [FromBody] Type entity)
    {
        var result = await crudBusiness.Create(entity);
        return Ok(result);
    }

    /// <summary>
    /// Delete route
    /// </summary>
    /// <param name="crudBusiness"></param>
    /// <param name="id"></param>
    /// <returns>Type</returns>
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromServices] IGenericCrudBusiness<Type, IdType> crudBusiness, [FromRoute] IdType id)
    {
        var result = await crudBusiness.Delete(id);
        return Ok(result);
    }
    
    /// <summary>
    /// Update route
    /// </summary>
    /// <param name="id"></param>
    /// <param name="crudBusiness"></param>
    /// <returns>Type</returns>
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Update([FromServices] IGenericCrudBusiness<Type, IdType> crudBusiness, [FromRoute] IdType id, [FromBody] Type entity)
    {
        var result = await crudBusiness.Update(id, entity);
        return Ok(result);
    }
    
    /// <summary>
    /// Read one route
    /// </summary>
    /// <returns>Type</returns>
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> Read([FromServices] IGenericCrudBusiness<Type, IdType> crudBusiness, [FromRoute] IdType id)
    {
        var result = await crudBusiness.Read(id);
        return Ok(result);
    }
    
    /// <summary>
    /// Read many route
    /// </summary>
    /// <returns>Type</returns>
    [HttpGet("")]
    [Authorize]
    public async Task<IActionResult> Read([FromServices] IGenericCrudBusiness<Type, IdType> crudBusiness)
    {
        var result = await crudBusiness.Read();
        return Ok(result);
    }
}