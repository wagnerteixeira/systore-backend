using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Systore.CrossCutting;

namespace Systore.Api.Configurations;

public static class AppConfig
{
    public static ApplicationConfig AddAppConfig(this IServiceCollection services, IConfiguration source)
    {
        var applicationConfig = source.Get<ApplicationConfig>();
        services.AddSingleton(applicationConfig);
        return applicationConfig;
    }
}