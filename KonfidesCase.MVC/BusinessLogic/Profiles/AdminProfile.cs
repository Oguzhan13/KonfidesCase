using AutoMapper;
using KonfidesCase.MVC.Areas.Admin.ViewModels;
using KonfidesCase.MVC.Areas.User.ViewModels;

namespace KonfidesCase.MVC.BusinessLogic.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<ActivityVM, ConfirmActivityVM>();
            
        }
    }
}
