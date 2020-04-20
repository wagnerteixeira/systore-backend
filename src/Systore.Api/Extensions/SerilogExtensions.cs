using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;

namespace Systore.Api.Extensions
{
    public static class SerilogExtensions
    {
        public static IServiceCollection UseSerilog(this IServiceCollection services,
            Serilog.ILogger logger = null, bool dispose = false)
        {
            services.AddSingleton<ILoggerFactory>(new SerilogLoggerFactory());
            return services;
        }
    }
}
