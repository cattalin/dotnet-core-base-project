using Core.Database.Context;
using Core.Database.Entities;
using Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Database.Repositories
{
    public class UsersRepository : BaseRepository<User>
    {
        private readonly IServiceScope scope;
        private readonly DatabaseContext context;

        public UsersRepository(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            scope = serviceProvider.CreateScope();
            context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        }

        public IEnumerable<User> GetUsers()
        {
            var results = context.Users
                .Where(entity => entity.FirstName == "test")
                .ToList();

            return results;
        }
    }
}
