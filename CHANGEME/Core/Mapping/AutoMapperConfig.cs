using AutoMapper;
using Core.Database.Entities;
using Core.Dtos;
using Infrastructure.InfrastructureBase;

namespace Core.Mapping
{
    public class AutoMapperConfig : BaseAutoMapperConfig
    {
        public static void Initialize()
        {
            var MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();

                cfg.CreateMap<UserDto, User>();
            });

            Mapper = MapperConfiguration.CreateMapper();
        }

    }
}
