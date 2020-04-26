using FastReport.Data;
using Microsoft.AspNetCore.Builder;
using FastReport.Utils;
using Microsoft.Extensions.DependencyInjection;
using Systore.Domain.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Systore.Report
{
    public static class Bootstrapper
    {
        public static IServiceCollection UseReport(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IReport>(new Report(configuration));
            return services;
        }

        public static IApplicationBuilder UseReport(this IApplicationBuilder app)
        {
            RegisteredObjects.AddConnection(typeof(MySqlDataConnection));
            app.UseFastReport();
            return app;
        }
    }
}
