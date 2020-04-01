using Core.Database.Entities;
using Core.Models;

namespace Core.Mapping
{
    public static class UserMappingExtensions
    {
        public static User ToEntity(this UserDto dto)
        {
            var mapper = AutoMapperConfig.Mapper;
            return mapper.Map<User>(dto);
        }

        public static UserDto ToDto(this User entity)
        {
            var mapper = AutoMapperConfig.Mapper;
            return mapper.Map<UserDto>(entity);
        }
    }
}
