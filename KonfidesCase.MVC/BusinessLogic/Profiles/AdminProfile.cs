using AutoMapper;

namespace KonfidesCase.MVC.BusinessLogic.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<Areas.Admin.ViewModels.ActivityVM, Areas.Admin.ViewModels.ActivityDetailVM>();
            
        }
    }
}
