using KonfidesCase.MVC.Areas.User.ViewModels;

namespace KonfidesCase.MVC.BusinessLogic.Profiles
{
    public class UserProfile : Profile
    {
        #region Constructor
        public UserProfile()
        {
            CreateMap<ActivityVM, ActivityDetailVM>();
            
        }
        #endregion
    }
}
