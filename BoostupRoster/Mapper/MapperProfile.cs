using AutoMapper;
using Boostup.API.Entities;
using Boostup.API.Entities.Dtos.Response;

namespace Boostup.API.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<User, UserResponse>();
        }
    }
}
