namespace KonfidesCase.Authentication.BusinessLogic.Profiles
{
    public class AuthProfile : Profile
    {
        #region Constructor
        public AuthProfile()
        {
            CreateMap<RegisterDto, AuthUser>().AfterMap((register, user) =>
            {                
                user.NormalizedEmail = register.Email.ToUpper(CultureInfo.GetCultureInfo("en-US"));
                user.UserName = user.Email;
                user.NormalizedUserName = user.NormalizedEmail;
            });            
        }
        #endregion
    }
}
