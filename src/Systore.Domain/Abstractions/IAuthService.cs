using System.Threading.Tasks;
using Systore.Dtos;

namespace Systore.Domain.Abstractions
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        LoginResponseDto ValidateToken(string token);
    }
}
