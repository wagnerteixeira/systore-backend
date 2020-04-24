using System;
using Microsoft.AspNetCore.Mvc;
using Systore.Domain.Abstractions;
using Systore.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;
using Newtonsoft.Json;
using Systore.Api.Exceptions;
using Systore.Domain;

namespace Systore.Api.Controllers
{
    [Route("oapi")]
    public class AuthController : ControllerBase, IDisposable
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        private readonly AppSettings _appSettings;

        public AuthController(IAuthService authService, ILogger<AuthController> logger, IConfiguration configuration)
        {
            _authService = authService;
            _logger = logger;
            _appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            try
            {
                var validationRelease = await VerifyRelease();

                if (!validationRelease.Release)
                {                      
                    return StatusCode(402, new LoginResponseDto()
                    {
                        Valid = false,
                        Relese = false
                    });
                }

                var result = await _authService.Login(loginRequestDto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return SendBadRequest(e);
            }
        }

        [AllowAnonymous]
        [HttpPost("validateToken")]
        public async Task<IActionResult> ValidateToken([FromBody] string token)
        {
            try
            {
                var validationRelease = await VerifyRelease();

                if (!validationRelease.Release)
                {
                    return StatusCode(402, new LoginResponseDto()
                    {
                        Valid = false,
                        Relese = false
                    });
                }

                var result = await Task.Run(() => _authService.ValidateToken(token));
                return Ok(result);
            }
            catch (Exception e)
            {
                return SendBadRequest(e);
            }
        }

        private async Task<ValidationReleaseDto> VerifyRelease()
        {
            var client = new RestClient(_appSettings.UrlRelease);

            var request = new RestRequest(Method.POST);
            request.AddParameter("clientId", _appSettings.ClientId);

            IRestResponse response = await client.ExecuteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<ValidationReleaseDto>(response.Content);
            else
                throw new VerifyReleaseException($"Erro ao verificar licença {response.StatusCode} {response.ErrorMessage} ");
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                Dispose(false);
            }
            _disposed = true;
        }

        protected IActionResult SendBadRequest(Exception e)
        {
            _logger.LogError(e, "Exception error: ");
            string error = e.Message;
            if (e.InnerException != null)
            {
                error += '|' + e.InnerException.Message;
                if (e.InnerException.InnerException != null)
                    error += '|' + e.InnerException.InnerException.Message;

            }

            return BadRequest(new { errors = error.Split('|') });
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}