using AutoMapper;
using KonfidesCase.MVC.Areas.User.ViewModels;

namespace KonfidesCase.MVC.BusinessLogic.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ActivityVM, ActivityDetailVM>();
            
        }
    }
}
