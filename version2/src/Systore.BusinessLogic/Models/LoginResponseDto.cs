namespace Systore.BusinessLogic.Models;

public record LoginResponseDto(UserLoginDto? User, string Token , bool Valid , bool Relese);