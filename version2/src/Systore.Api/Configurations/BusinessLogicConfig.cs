using Systore.BusinessLogic;
using Systore.BusinessLogic.Interfaces;

namespace Systore.Api.Configurations;

public static class BusinessLogicConfig
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
    {
        return services
            .AddSingleton<IAuthentication, Authentication>();
    }
}