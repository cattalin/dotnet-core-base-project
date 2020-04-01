using AutoMapper;
using Core.Database.Entities;
using Core.Models;
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
