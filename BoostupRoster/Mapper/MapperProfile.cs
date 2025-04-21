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
            CreateMap<EmployeeDetail, EmployeeBasicResponse>()
                .ForMember(dest => dest.EmployeeId , opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.User.FullName));
            CreateMap<Leave, LeaveResponse>()
                .ForMember(dest => dest.Employee , opt => opt.MapFrom(src => src.Employee))
                .ForMember(dest => dest.LeaveType , opt => opt.MapFrom(src => src.LeaveType));
            CreateMap<Roster, RosterResponse>()
                .ForMember(dest => dest.Employee, opt => opt.MapFrom(src => src.Employee))
                .ForMember(dest => dest.Leaves , opt => opt.MapFrom(src => src.Leaves));
        }


    }
}
