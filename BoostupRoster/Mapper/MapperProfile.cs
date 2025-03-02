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
            CreateMap<EmployeeDetail, EmployeeDetailResponse>();
            CreateMap<Jobs, JobResponseBasic>();
            CreateMap<Roster, RosterBasicResponse>()
                .ForMember(dest => dest.Job , opt=> opt.MapFrom(src => src.Job));
            CreateMap<EmployeeDetail, EmployeeWithRosterResponse>()
                .ForMember(dest => dest.RosterItems, opt => opt.MapFrom(src => src.Rosters));
        }
    }
}
