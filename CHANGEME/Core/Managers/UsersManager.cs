using Core.Database.Entities;
using Core.Database.Repositories;
using Infrastructure.Base;
using Core.Dtos;

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
