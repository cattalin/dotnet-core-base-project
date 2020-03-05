using Api.Middlewares;
using AutoMapper;
using Core.Config;
using Core.Database.Context;
using Core.Database.Repositories;
using Core.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            var runtimeSettings = Configuration.GetSection("RuntimeSettings");
            services.Configure<RuntimeSettings>(runtimeSettings);
            
            services.AddAutoMapper(c => c.AddProfile<AutoMappingProfile>(), typeof(Startup));

            services.AddControllers();

            AddCoreDependencyInjections(services);
        }

        private void AddCoreDependencyInjections(IServiceCollection services)
        {
            //var connectionString = Configuration.GetSection("RuntimeSettings.ConnectionString").Value;
            //services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));
            services.AddTransient<DatabaseContext>();
            services.AddTransient<DbContext, DatabaseContext>();

            services.AddTransient<UsersManager>();

            services.AddTransient<UsersRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseExceptionHandlingMiddleware();
            app.UseLogRequestId();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
