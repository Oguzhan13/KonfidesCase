namespace KonfidesCase.MVC.BusinessLogic.Profiles
{
    public class AdminProfile : Profile
    {
        #region Constructor
        public AdminProfile()
        {
            CreateMap<Areas.Admin.ViewModels.ActivityVM, Areas.Admin.ViewModels.ActivityDetailVM>();
            
        }
        #endregion
    }
}
