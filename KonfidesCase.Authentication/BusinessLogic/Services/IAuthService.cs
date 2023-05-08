namespace KonfidesCase.Authentication.BusinessLogic.Services
{
    public interface IAuthService
    {
        #region Methods for Actions
        Task<UserInfoDto> CreateAppUserInfo(AuthUser user, string password);
        Task<AuthDataResult<AuthUser>> LoginCheck(LoginDto loginDto);
        Task<AuthDataResult<UserInfoDto>> LoginResponse(AuthUser currentUser, string password);
        Task<AuthDataResult<UserInfoDto>> Register(RegisterDto registerDto);
        Task<AuthDataResult<UserInfoDto>> ChangePassword(string currentUserName, ChangePasswordDto changePasswordDto);        
        #endregion
    }
}
