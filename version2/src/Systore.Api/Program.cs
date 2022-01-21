using System.Reflection;
using Microsoft.OpenApi.Models;
using Systore.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

// builder.AddLogging();

// Add services to the container.

builder.Services.AddControllers();

var applicationConfig =  builder.Services.AddAppConfig(builder.Configuration);

builder.Services
    .AddSerilog(builder.Configuration)
    .AddRepositories(applicationConfig)
    .AddBusiness()
    .AddJwtAuthentication(applicationConfig)
    .AddSwagger()
    .Configure<RouteOptions>(options => options.LowercaseUrls = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Local"))
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "Systore API");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();