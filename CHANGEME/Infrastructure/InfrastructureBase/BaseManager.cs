using AutoMapper;
using Infrastructure.InfrastructureBase;
using Infrastructure.MappingBase;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Infrastructure.Base
{
    public class BaseManager
    {
        protected IMapper mapper { get; set; }

        public BaseManager()
        {
            mapper = BaseAutoMapperConfig.Mapper;
        }
    }

    public class BaseManager<TRepo, TEntity, TDto> where TRepo : BaseRepository<TEntity> where TEntity : BaseEntity where TDto : BaseDto
    {
        protected ILogger logger { get; set; }
        protected IMapper mapper { get; set; }
        protected TRepo mainRepository { get; set; }

        public BaseManager(TRepo mainRepository)
        {
            this.mainRepository = mainRepository;
            mapper = BaseAutoMapperConfig.Mapper;
        }

        public TDto GetRawById(int id)
        {
            var entity = mainRepository.GetRawById(id);
            var dto = mapper.Map<TDto>(entity);

            return dto;
        }

        public List<TDto> GetRawList(int skip, int take)
        {
            var entities = mainRepository.GetRawList(skip, take);
            var dtos = mapper.Map<List<TDto>>(entities);

            return dtos;
        }

        public TDto CreateNew(TDto newDto)
        {
            var entity = newDto.ToEntity<TEntity>();
            var newEntity = mainRepository.Insert(entity);

            return newEntity.ToDto<TDto>();
        }
    }
}
