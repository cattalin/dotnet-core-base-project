using AutoMapper;

namespace Infrastructure.InfrastructureBase
{
    public abstract class BaseAutoMapperConfig
    {
        public static IMapper Mapper { get; protected set; }
    }
}
