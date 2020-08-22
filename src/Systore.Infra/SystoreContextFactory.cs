using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using Systore.Domain;
using Systore.Infra.Context;

namespace Systore.Infra
{
    public class SystoreContextFactory : IDesignTimeDbContextFactory<SystoreContext>
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
           .Build();

        public SystoreContext CreateDbContext(string[] args)
        {
            var settingsSection = Configuration.GetSection("AppSettings");
            var appSettings = new AppSettings();
            settingsSection.Bind(appSettings);

            var optionsBuilder = new DbContextOptionsBuilder<SystoreContext>();
            optionsBuilder.UseMySql(Configuration.GetConnectionString("Systore"));
            IOptions<AppSettings> options = Options.Create(appSettings);
            return new SystoreContext(optionsBuilder.Options, options, Configuration);
        }
    }
}