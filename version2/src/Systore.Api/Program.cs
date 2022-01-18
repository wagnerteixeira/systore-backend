using System.Reflection;
using Microsoft.OpenApi.Models;
using Systore.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

// builder.AddLogging();

// Add services to the container.

builder.Services.AddControllers();

var applicationConfig =  builder.Services.AddAppConfig(builder.Configuration);

builder.Services.AddSerilog(builder.Configuration);
builder.Services.AddRepositories(applicationConfig);
builder.Services.AddBusinessLogic();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
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
                    Id = "bearerAuth" 
                }
            },
            new string[] {}
        }
    });
    
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

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

app.UseAuthorization();

app.MapControllers();

app.Run();