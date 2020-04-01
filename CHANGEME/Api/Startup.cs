using Api.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddControllers();
            services.AddAppSettings(Configuration);
            services.AddDbContext(Configuration);
            services.AddJwtAuthentication();
            services.AddAutoMapper();
            services.AddSwagger();
            services.AddCoreModules();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseExceptionHandlingMiddleware();
            app.UseLogRequestId();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAppSwagger();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
