using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Systore.Infra.Context;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Systore.Domain;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Http;
using Systore.Api.Extensions;
using Systore.Report;
using System.Globalization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Hosting;


namespace Systore.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppSettings _appSettings;
        private readonly IConfigurationSection _appSettingsSection;

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
            _appSettingsSection = Configuration.GetSection("AppSettings");
            _appSettings = _appSettingsSection.Get<AppSettings>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .UseRepositories()
                .UseServices()
                .UseAutoMapper()
                .AddCors()
                .UseReport(Configuration)
                .AddControllers();




            services.AddDbContext<SystoreContext>(options =>
             {
                 if (_appSettings.DatabaseType == "MySql")
                     options.UseMySql(Configuration.GetConnectionString("Systore"));
                 else if (_appSettings.DatabaseType == "InMem")
                     options.UseInMemoryDatabase("systore");
                 options.EnableSensitiveDataLogging();
             }).AddDbContext<AuditContext>(options =>
             {
                 if (_appSettings.DatabaseType == "MySql")
                     options.UseMySql(Configuration.GetConnectionString("SystoreAudit"));
                 else if (_appSettings.DatabaseType == "InMem")
                     options.UseInMemoryDatabase("systoreAudit");
                 options.EnableSensitiveDataLogging();
             });

            // TODO unify this lines
            Console.WriteLine($"Enviroment {_env.EnvironmentName}");

            Console.WriteLine($"Systore ConnectionString: {Configuration.GetConnectionString("Systore")}");

            Console.WriteLine($"SystoreAudit ConnectionString: {Configuration.GetConnectionString("SystoreAudit")}");

            services.Configure<AppSettings>(_appSettingsSection);

            if (_env.IsDevelopment())
            {
                services.AddMvc(opts =>
                {
                    opts.Filters.Add(new AllowAnonymousFilter());
                }).AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            }
            else
                services.AddMvc()
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.Formatting = Formatting.Indented;
                        options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);



            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);           

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Systore", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,

                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },       
                            
                           // In = ParameterLocation.Header
                        },
                        new[] { "readAccess", "writeAccess" }
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            CultureInfo.CurrentCulture = cultureInfo;

            // Configure the Localization middleware
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(cultureInfo),
                SupportedCultures = new List<CultureInfo>
            {
                cultureInfo,
            },
                SupportedUICultures = new List<CultureInfo>
            {
                cultureInfo,
            }
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()
            )
            .UseSwagger()// Enable middleware to serve generated Swagger as a JSON endpoint.
            .UseSwaggerUI(c =>
            {                                                             // specifying the Swagger JSON endpoint.
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Systore");// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),                                                                         
            })
            //.UseMetrics()
            .UseReport();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            Console.WriteLine($"Current culture: {CultureInfo.CurrentCulture}");
            Console.WriteLine($"Local timezone {TimeZoneInfo.Local} Utc {TimeZoneInfo.Utc}");

            // uncoment for automatic migration            
            InitializeDatabase(app);

        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                if (_appSettings.DatabaseType == "Mysql")
                {
                    scope.ServiceProvider.GetRequiredService<SystoreContext>().Database.Migrate();
                    scope.ServiceProvider.GetRequiredService<AuditContext>().Database.Migrate();
                }
                else if (_appSettings.DatabaseType == "InMem")
                {
                    scope.ServiceProvider.GetRequiredService<SystoreContext>().Database.EnsureCreated();
                    scope.ServiceProvider.GetRequiredService<AuditContext>().Database.EnsureCreated();
                }
            }
        }
    }
}

