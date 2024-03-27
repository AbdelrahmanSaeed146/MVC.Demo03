using AutoMapper;
using MVC.Demo03.DAL.Models;
using MVC.Demo03.PL.Models;

namespace MVC.Demo03.PL.Helpers
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
