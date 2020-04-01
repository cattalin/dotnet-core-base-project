using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Infrastructure.Config
{
    public static class AppConfig
    {
        public static RuntimeSettings RuntimeSettings { get; set; }
        public static ConnectionStringSettings ConnectionStrings { get; set; }
        public static AuthSettings AuthSettings { get; set; }

        public static void Init(IConfiguration Configuration)
        {
            Configure(Configuration);
        }

        public static void MigrationsInit()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .SetBasePath($"{Directory.GetCurrentDirectory()}/../Api/")
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env}.json");

            var Configuration = builder.Build();
            Configure(Configuration);
        }

        private static void Configure(IConfiguration Configuration)
        {
            var runtimeSettings = Configuration.GetSection("RuntimeSettings");
            RuntimeSettings = runtimeSettings.Get<RuntimeSettings>();

            var connectionStringSettings = Configuration.GetSection("ConnectionStrings");
            ConnectionStrings = connectionStringSettings.Get<ConnectionStringSettings>();

            var authSettings = Configuration.GetSection("AuthSettings");
            AuthSettings = authSettings.Get<AuthSettings>();
        }
    }
}
