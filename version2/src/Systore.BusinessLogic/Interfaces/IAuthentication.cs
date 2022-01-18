using Systore.BusinessLogic.Models;

namespace Systore.BusinessLogic.Interfaces;

public interface IAuthentication
{
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
}