using Core.Database.Entities;
using Core.Database.Repositories;
using Infrastructure.Base;
using Core.Models;
using Microsoft.Extensions.Logging;

namespace Core.Managers
{
    public class UsersManager : BaseManager<UsersRepository, User, UserDto>
    {
        public UsersManager(
            UsersRepository mainRepository,

            ILogger<UsersRepository> logger
        ) : base(mainRepository, logger)
        {
        }
    }
}
