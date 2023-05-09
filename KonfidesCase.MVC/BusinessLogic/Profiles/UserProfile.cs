using AutoMapper;
using KonfidesCase.MVC.Areas.User.Models;
using KonfidesCase.MVC.Areas.User.ViewModels;

namespace KonfidesCase.MVC.BusinessLogic.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Activity, ActivityDetailVM>();
            
        }
    }
}
