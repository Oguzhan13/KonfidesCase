namespace KonfidesCase.BLL.Profiles
{
    public class AdminProfiles : Profile
    {
        #region Constructor
        public AdminProfiles()
        {
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>().ReverseMap();
            CreateMap<CreateCityDto, City>();
            CreateMap<UpdateCityDto, City>().ReverseMap();
            CreateMap<CreateActivityDto, Activity>();
            CreateMap<UpdateActivityDto, Activity>().ReverseMap();
        }
        #endregion
    }
}
