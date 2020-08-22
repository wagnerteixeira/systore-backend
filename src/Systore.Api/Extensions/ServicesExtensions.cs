using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Systore.Api.AutoMapperProfiles;
using Systore.Domain.Abstractions;
using Systore.Services;

namespace Systore.Api.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection UseServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IBillReceiveService, BillReceiveService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<IAuditService, AuditService>();
            services.AddScoped<ICalculateValuesClothingStoreService, CalculateValuesClothingStoreService>();

            return services;
        }

        public static IServiceCollection UseAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup), typeof(SystoreApiProfile));
            return services;
        }
    }
}

