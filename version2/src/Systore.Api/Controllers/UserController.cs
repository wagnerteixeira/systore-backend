using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Systore.Business.Interfaces;
using Systore.CrossCutting.Models;
using ILogger = Serilog.ILogger;

namespace Systore.Api.Controllers;

/// <summary>
/// User Controller
/// </summary>
/// [ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger _logger;
    
    /// <summary> 
    /// </summary>
    /// <param name="logger"></param>
    public UserController(ILogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Create route
    /// </summary>
    /// <param name="user"></param>
    /// <returns>User</returns>
    [HttpPost("")]
    [Authorize]
    public async Task<IActionResult> Create([FromServices] IUserBusiness userBusiness, [FromBody] User user)
    {
        var result = await userBusiness.Create(user);
        return Ok(result);
    }

    /// <summary>
    /// Create route
    /// </summary>
    /// <param name="id"></param>
    /// <returns>User</returns>
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromServices] IUserBusiness userBusiness, [FromRoute] int id)
    {
        var result = await userBusiness.Delete(id);
        return Ok(result);
    }
    
    /// <summary>
    /// Create route
    /// </summary>
    /// <returns>User</returns>
    [HttpPut("")]
    [Authorize]
    public async Task<IActionResult> Update([FromServices] IUserBusiness userBusiness, [FromBody] User user)
    {
        var result = await userBusiness.Update(user);
        return Ok(result);
    }
    
    /// <summary>
    /// Read one route
    /// </summary>
    /// <returns>User</returns>
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> Read([FromServices] IUserBusiness userBusiness, [FromRoute] int id)
    {
        var result = await userBusiness.Read(id);
        return Ok(result);
    }
    
    /// <summary>
    /// Read many route
    /// </summary>
    /// <returns>User</returns>
    [HttpGet("")]
    [Authorize]
    public async Task<IActionResult> Read([FromServices] IUserBusiness userBusiness)
    {
        var result = await userBusiness.Read();
        return Ok(result);
    }
}