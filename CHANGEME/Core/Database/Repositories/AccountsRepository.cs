using Core.Database.Context;
using Core.Database.Entities;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Core.Database.Repositories
{
    public class AccountsRepository : BaseRepository<Account>
    {
        private readonly IServiceScope scope;
        private readonly DatabaseContext context;

        public AccountsRepository(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            scope = serviceProvider.CreateScope();
            context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        }

        public Account FindByEmail(string email)
        {
            var entity = context.Accounts
                .Where(x => x.Email == email)
                .AsNoTracking()
                .FirstOrDefault();

            return entity;
        }
    }
}
