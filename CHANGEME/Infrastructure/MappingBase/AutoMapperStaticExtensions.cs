using Infrastructure.Base;
using Infrastructure.InfrastructureBase;
using System.Collections.Generic;

namespace Infrastructure.MappingBase
{
    public static class AutoMapperStaticExtensions
    {
        public static TEntity ToEntity<TEntity>(this BaseDto dto)
        {
            var mapper = BaseAutoMapperConfig.Mapper;
            return mapper.Map<TEntity>(dto);
        }

        public static TDto ToDto<TDto>(this BaseEntity entity)
        {
            var mapper = BaseAutoMapperConfig.Mapper;
            return mapper.Map<TDto>(entity);
        }

        public static List<TEntity> ToEntities<TEntity>(this IEnumerable<BaseDto> dto)
        {
            var mapper = BaseAutoMapperConfig.Mapper;
            return mapper.Map<List<TEntity>>(dto);
        }

        public static List<TDto> ToDtos<TDto>(this IEnumerable<BaseEntity> entity)
        {
            var mapper = BaseAutoMapperConfig.Mapper;
            return mapper.Map<List<TDto>>(entity);
        }
    }
}
