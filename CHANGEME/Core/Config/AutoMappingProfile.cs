using AutoMapper;
using Core.Database.Entities;
using Core.Models;

namespace Core.Config
{
    public class AutoMappingProfile: Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
