using Microsoft.AspNetCore.Mvc;
using Systore.BusinessLogic.Interfaces;
using Systore.BusinessLogic.Models;
using ILogger = Serilog.ILogger;

namespace Systore.Api.Controllers;

/// <summary>
/// Athenttication Controller
/// </summary>
[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger _logger;

    /// <summary> 
    /// </summary>
    /// <param name="logger"></param>
    public AuthenticationController(ILogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Login route
    /// </summary>
    /// <param name="loginRequestDto"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromServices] IAuthentication authentication, [FromBody] LoginRequestDto loginRequestDto)
    {
        var result = await authentication.Login(loginRequestDto);
        return Ok(result);
    }
}