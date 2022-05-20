using Systore.Business.Models;

namespace Systore.Business.Interfaces;

public interface IAuthenticationBusiness
{
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
}