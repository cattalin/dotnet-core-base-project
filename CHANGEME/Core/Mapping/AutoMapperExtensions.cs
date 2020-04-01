using AutoMapper;
using Core.Database.Entities;
using Core.Models;

namespace Core.Mapping
{
    public static class AutoMapperExtensions
    {
        public static User ToEntity(this IMapper mapper, UserDto dto)
        {
            return mapper.Map<User>(dto);
        }

        public static TEntity ToEntity<TEntity, TDto>(this IMapper mapper, TDto dto)
        {
            return mapper.Map<TEntity>(dto);
        }

        public static TDto ToDto<TEntity, TDto>(this IMapper mapper, TEntity entity)
        {
            return mapper.Map<TDto>(entity);
        }
    }
}
