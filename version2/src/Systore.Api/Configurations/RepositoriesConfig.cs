using System.Data.Common;
using MySql.Data.MySqlClient;
using Systore.CrossCutting;
using Systore.Repositories;
using Systore.Repositories.Interfaces;

namespace Systore.Api.Configurations;


public static class RepositoriesConfig
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, ApplicationConfig applicationConfig)
    {
        services.AddHttpClient<IReleaseRepository, ReleaseRepository>(client =>
        {
            client.BaseAddress = applicationConfig.ReleaseConfig.BaseUrl;
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        services.AddSingleton<DbProviderFactory, MySqlClientFactory>();

        return services
            .AddSingleton<IDatabaseFactory, DatabaseFactory>()
            .AddSingleton<IUserRepository, UserRepository>();

    }
}