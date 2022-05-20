using System.Data.Common;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using Systore.CrossCutting;
using Systore.CrossCutting.Models;
using Systore.Repositories;
using Systore.Repositories.Interfaces;

namespace Systore.Api.Configurations;

public static class RepositoriesConfig
{
    public static IServiceCollection AddRepositories(this IServiceCollection services,
        ApplicationConfig applicationConfig)
    {
        services.AddHttpClient(applicationConfig);

        services.AddDatabaseConfig();

        services.AddSqlStatements();

        return services.AddRepositories();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped(typeof(IGenericCrudRepository<,>), typeof(GenericCrudRepository<,>))
            .AddScoped<IUserRepository, UserRepository>();
    }

    private static void AddDatabaseConfig(this IServiceCollection services)
    {
        services.AddSingleton<DbProviderFactory, MySqlClientFactory>()
            .AddSingleton<IDatabaseFactory, DatabaseFactory>();
    }

    private static void AddHttpClient(this IServiceCollection services,
        ApplicationConfig applicationConfig)
    {
        services.AddHttpClient<IReleaseRepository, ReleaseRepository>(client =>
        {
            client.BaseAddress = applicationConfig.ReleaseConfig.BaseUrl;
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });
    }

    private static void AddSqlStatements(this IServiceCollection services)
    {
        services
            .AddSingleton(_ =>
                new SqlStatements<User>(
                    UserSqlStatements.CreateSqlStatement,
                    UserSqlStatements.SelectSingleSqlStatement,
                    UserSqlStatements.SelectAllSqlStatement,
                    UserSqlStatements.DeleteSqlStatement,
                    UserSqlStatements.UpdateSqlStatement)
            ).AddSingleton(_ =>
                new SqlStatements<Client>(
                    ClientSqlStatements.CreateSqlStatement,
                    ClientSqlStatements.SelectSingleSqlStatement,
                    ClientSqlStatements.SelectAllSqlStatement,
                    ClientSqlStatements.DeleteSqlStatement,
                    ClientSqlStatements.UpdateSqlStatement)
            ).AddSingleton(_ =>
                new SqlStatements<Sale>(
                    SaleSqlStatements.CreateSqlStatement,
                    SaleSqlStatements.SelectSingleSqlStatement,
                    SaleSqlStatements.SelectAllSqlStatement,
                    SaleSqlStatements.DeleteSqlStatement,
                    SaleSqlStatements.UpdateSqlStatement)
            ).AddSingleton(_ =>
                new SqlStatements<Product>(
                    ProductSqlStatements.CreateSqlStatement,
                    ProductSqlStatements.SelectSingleSqlStatement,
                    ProductSqlStatements.SelectAllSqlStatement,
                    ProductSqlStatements.DeleteSqlStatement,
                    ProductSqlStatements.UpdateSqlStatement)
            ).AddSingleton(_ =>
                new SqlStatements<BillReceive>(
                    BillReceiveSqlStatements.CreateSqlStatement,
                    BillReceiveSqlStatements.SelectSingleSqlStatement,
                    BillReceiveSqlStatements.SelectAllSqlStatement,
                    BillReceiveSqlStatements.DeleteSqlStatement,
                    BillReceiveSqlStatements.UpdateSqlStatement)
            );
    }
}