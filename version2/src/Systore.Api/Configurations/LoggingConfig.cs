using Serilog;
using ILogger = Serilog.ILogger;

namespace Systore.Api.Configurations;

public static class LoggingConfig
{
    public static IServiceCollection AddSerilog(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddLogging((builder) =>
        {
            builder.ClearProviders();
            var loggerConfig = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration);
            var logger = loggerConfig.CreateLogger();
            builder.AddSerilog(logger);
            services.AddSingleton<ILogger>(logger);
        });
    }
}