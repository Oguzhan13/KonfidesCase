namespace KonfidesCase.Authentication.BusinessLogic.Services
{
    public interface IAuthService
    {
        #region Methods
        Task<AuthDataResult<RegisterDto>> Login(LoginDto loginDto);        
        Task<AuthDataResult<AuthUser>> Register(RegisterDto registerDto);
        Task<AuthDataResult<RegisterDto>> ChangePassword(ChangePasswordDto changePasswordDto);
        Task Logout();
        #endregion
    }
}
