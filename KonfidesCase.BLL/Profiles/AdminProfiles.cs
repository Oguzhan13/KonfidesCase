using AutoMapper;
using KonfidesCase.DTO.Activity;
using KonfidesCase.DTO.Category;
using KonfidesCase.DTO.City;
using KonfidesCase.Entity.Entities;

namespace KonfidesCase.BLL.Profiles
{
    public class AdminProfiles : Profile
    {
        public AdminProfiles()
        {
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>().ReverseMap();
            CreateMap<CreateCityDto, City>();
            CreateMap<UpdateCityDto, City>().ReverseMap();
            CreateMap<CreateActivityDto, Activity>();
            CreateMap<UpdateActivityDto, Activity>().ReverseMap();
        }
    }
}
