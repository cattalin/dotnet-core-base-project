using Infrastructure.Config;
using Core.Database.Context;
using Core.Database.Repositories;
using Core.Managers;
using Core.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Api
{
    public static class StartupConfig
    {
        public static void AddAppSettings(this IServiceCollection services, IConfiguration Configuration)
        {
            var runtimeSettings = Configuration.GetSection("RuntimeSettings");
            services.Configure<RuntimeSettings>(runtimeSettings);

            AppConfig.Init(Configuration);
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration Configuration)
        {
            var connectionString = Configuration.GetConnectionString("AzureDatabase");
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));
            services.AddTransient<DatabaseContext>();
            services.AddTransient<DbContext, DatabaseContext>();
        }
        
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mankind Api", Version = "v1" });
            });
        }

        public static void UseAppSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mankind Api");
            });
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            AutoMapperConfig.Initialize();
        }

        public static void AddCoreModules(this IServiceCollection services)
        {
            services.AddTransient<UsersManager>();

            services.AddTransient<UsersRepository>();
        }

        public static void AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = AppConfig.AuthSettings.JwtKey,
                     ValidAudience = AppConfig.AuthSettings.JwtIssuer,

                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfig.AuthSettings.JwtKey))
                 };
             });
        }
    }
}
