using Core.Database.Entities;
using Core.Database.Repositories;
using Core.Dtos;
using Infrastructure.Base;

namespace Core.Managers
{
    public class UsersManager : BaseManager<UsersRepository, User, UserDto>
    {
        public UsersManager(
            UsersRepository mainRepository
        ) : base(mainRepository)
        {
        }
    }
}
