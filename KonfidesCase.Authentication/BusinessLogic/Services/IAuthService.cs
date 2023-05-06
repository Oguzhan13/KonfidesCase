namespace KonfidesCase.Authentication.BusinessLogic.Services
{
    public interface IAuthService
    {
        #region Methods for Actions
        Task<AuthDataResult<UserInfoDto>> Login(LoginDto loginDto);        
        Task<AuthDataResult<UserInfoDto>> Register(RegisterDto registerDto);
        Task<AuthDataResult<UserInfoDto>> ChangePassword(ChangePasswordDto changePasswordDto);
        Task Logout();
        #endregion
    }
}
