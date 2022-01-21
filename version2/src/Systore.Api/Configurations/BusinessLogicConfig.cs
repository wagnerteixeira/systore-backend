using Systore.Business;
using Systore.Business.Interfaces;

namespace Systore.Api.Configurations;

public static class BusinessConfig
{
    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        return services
            .AddSingleton<IAuthenticationBusiness, AuthenticationBusiness>()
            .AddSingleton<IUserBusiness, UserBusiness>();
    }
}