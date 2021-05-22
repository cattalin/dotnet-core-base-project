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
            services.AddControllers().AddJsonSerializer();
            services.AddAppSettings(Configuration);
            services.AddDbContext(Configuration);

            services.AddAutoMapper();

            services.AddSwagger();
            services.AddJwtAuthentication();
            services.AddHealthChecks();

            services.AddCoreManagers();
            services.AddCoreRepositories();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseMiddleware<ExceptionHandlingMiddleware>();

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
