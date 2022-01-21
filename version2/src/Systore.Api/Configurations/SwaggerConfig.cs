using System.Reflection;
using Microsoft.OpenApi.Models;

namespace Systore.Api.Configurations;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        return services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v2", new OpenApiInfo
            {
                Version = "v2",
                Title = "Systore API",
                Description = "Store application",
                TermsOfService = new Uri("https://systore.com.br/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Wagner Bernardes Teixeira",
                    Url = new Uri("https://systore.com.br/contact")
                },
                License = new OpenApiLicense
                {
                    Name = "Example License",
                    Url = new Uri("https://systore.com.br/license")
                }
            });
            
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            });
            
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference 
                        { 
                            Type = ReferenceType.SecurityScheme, 
                            Id = "Bearer" 
                        },
                    },
                    Array.Empty<string>()
                }
            });
            
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }
}