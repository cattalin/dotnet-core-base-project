using Core.Database.Context;
using Core.Database.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.Database.Repositories
{
    public class DataSeedingRepository
    {
        private readonly IServiceScope scope;
        private readonly DatabaseContext context;

        public DataSeedingRepository(IServiceProvider serviceProvider)
        {
            scope = serviceProvider.CreateScope();
            context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        }

        public Company SeedCompany()
        {
            var company = new Company
            {
                Name = "Test Company",
                Description = "Test Description Test Description Test Description Test Description Test Description",
            };

            context.Companies.Add(company);
            context.SaveChanges();

            return company;
        }
    }
}
