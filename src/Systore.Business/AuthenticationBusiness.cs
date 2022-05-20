using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Systore.Business.Interfaces;
using Systore.Business.Models;
using Systore.CrossCutting;
using Systore.Repositories.Interfaces;

namespace Systore.Business;

public class AuthenticationBusiness : IAuthenticationBusiness
{
    private readonly IUserRepository _userRepository;
    private readonly IReleaseRepository _releaseRepository;
    private readonly ApplicationConfig _applicationConfig;

    public AuthenticationBusiness(IUserRepository userRepository, IReleaseRepository releaseRepository, ApplicationConfig applicationConfig)
    {
        _userRepository = userRepository;
        _releaseRepository = releaseRepository;
        _applicationConfig = applicationConfig;
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        var release = await _releaseRepository.VerifyRelease(_applicationConfig.ReleaseConfig.ClientId);
        if (!release)
        {
            return new(null, "", false, false);
        }
        var (userName, password) = loginRequestDto;
        var user  = await _userRepository.GetUserByUsernameAndPassword(userName, password);
        if (user != null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_applicationConfig.Secret);
            var claims = new []{
                new Claim(ClaimTypes.Name, loginRequestDto.UserName),
                new Claim("admin", $"{user.Admin}")
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return new (new (loginRequestDto.UserName, user.Admin), token, true, release);
        }
        
        return new(null, "", false, release);
    }
}