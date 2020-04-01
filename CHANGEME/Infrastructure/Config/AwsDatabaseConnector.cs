using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using System;

namespace Infrastructure.Config
{
    public sealed class AwsDatabaseConnector
    {
        static readonly AwsDatabaseConnector instance = new AwsDatabaseConnector();

        public static AwsDatabaseConnector GetInstance()
        {
            return instance;
        }

        public string ConnectionString { get; }

        private AwsDatabaseConnector()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var parameterName = $"/ConnectionString/{environment}/Mankind";

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
