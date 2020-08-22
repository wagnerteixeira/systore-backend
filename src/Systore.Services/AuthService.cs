using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Systore.Data.Abstractions;
using Systore.Domain;
using Systore.Domain.Abstractions;
using Systore.Dtos;

namespace Systore.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;

        public AuthService(IUserRepository userRepository, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }

        public LoginResponseDto ValidateToken(string token, bool validateLifetime = true)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = validateLifetime
            };

            var claims = handler.ValidateToken(token, validations, out var tokenSecure);

            var adminClaim = claims.Claims.FirstOrDefault(c => c.Type == "admin");
            bool admin = false;
            if (adminClaim != null)
            {
                bool.TryParse(adminClaim.Value, out admin);
            }

            return new LoginResponseDto()
            {
                User = new UserLoginDto()
                {
                    Admin = admin,
                    UserName = claims.Identity.Name,
                },
                Token = token,
                Valid = true
            };
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _userRepository.GetUserByUsernameAndPassword(loginRequestDto.UserName, loginRequestDto.Password);
            if (user != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var claims = new Claim[]{
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("admin", user.Admin.ToString())
                };
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new LoginResponseDto()
                {
                    Valid = true,
                    Token = tokenHandler.WriteToken(token),
                    User = new UserLoginDto()
                    {
                        Admin = user.Admin,
                        UserName = user.UserName
                    }
                };
            }
            else
            {
                return new LoginResponseDto()
                {
                    Valid = false,
                    Relese = true
                };
            }
        }

    }
}