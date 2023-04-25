using AutoMapper;
using CleanArchitect.MVC.Models;
using CleanArchitect.MVC.Services.Base;

namespace CleanArchitect.MVC.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateLeaveTypeDto, CreateLeaveTypeVM>().ReverseMap();
            CreateMap<LeaveTypeDto, LeaveTypeVM>().ReverseMap();
        }
    }
}
