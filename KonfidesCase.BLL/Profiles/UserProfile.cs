namespace KonfidesCase.BLL.Profiles
{
    public class UserProfile : Profile
    {
        #region Constructor
        public UserProfile()
        {
            CreateMap<UserInfoDto, AppUser>()
                .ForSourceMember(uid => uid.Password, dst => dst.DoNotValidate());
            
        }
        #endregion
    }
}
