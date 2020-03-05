using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using System;

namespace Core.Infrastructure
{
    public sealed class DatabaseConnector
    {
        static readonly DatabaseConnector instance = new DatabaseConnector();

        public static DatabaseConnector GetInstance()
        {
            return instance;
        }

        public string ConnectionString { get; }

        private DatabaseConnector()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var parameterName = $"/ConnectionString/{environment}/CHANGEME";

            using (var client = new AmazonSimpleSystemsManagementClient())
            {
                var request = new GetParameterRequest() { Name = parameterName, WithDecryption = true };

                using (var response = client.GetParameterAsync(request))
                {
                    ConnectionString = response.Result.Parameter.Value;
                };
            }
        }
    }
}
