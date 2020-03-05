using AutoMapper;
using Core.Database.Repositories;
using Core.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Core.Managers
{
    public class UsersManager
    {
        private ILogger logger { get; set; }
        private IMapper mapper { get; set; }
        private UsersRepository usersRepository { get; set; }

        public UsersManager(
            UsersRepository usersRepository,

            IMapper mapper,
            ILogger<UsersRepository> logger
            )
        {
            this.usersRepository = usersRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public UserDto GetById(int id)
        {
            var userEntity = usersRepository.GetById(id);
            var userDto = mapper.Map<UserDto>(userEntity);

            return userDto;
        }
    }
}
