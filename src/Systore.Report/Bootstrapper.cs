using FastReport.Data;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Utils;
using Microsoft.Extensions.DependencyInjection;
using Systore.Domain.Abstractions;
using Systore.Domain;
using Microsoft.Extensions.Configuration;

namespace Systore.Report
{
    public static class Bootstrapper
    {
        public static IServiceCollection UseReport(
            this IServiceCollection services, 
            AppSettings appSettings, 
            IConfiguration configuration)
        {
            services.AddSingleton<IReport>(new Report(appSettings, configuration));
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
