using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Systore.Business.Interfaces;
using Systore.Business.Models;
using ILogger = Serilog.ILogger;

namespace Systore.Api.Controllers;

/// <summary>
/// Authentication Controller
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
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromServices] IAuthenticationBusiness authenticationBusiness, [FromBody] LoginRequestDto loginRequestDto)
    {
        var result = await authenticationBusiness.Login(loginRequestDto);
        return Ok(result);
    }
}