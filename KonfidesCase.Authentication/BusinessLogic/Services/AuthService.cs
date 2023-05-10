namespace KonfidesCase.Authentication.BusinessLogic.Services
{
    public class AuthService : IAuthService
    {
        #region Fields & Constructor
        private readonly UserManager<AuthUser> _userManager;        
        private readonly IMapper _mapper;
        public AuthService(UserManager<AuthUser> userManager, IMapper mapper)
        {
            _userManager = userManager;            
            _mapper = mapper;            
        }
        #endregion
        
        #region Methods for Actions
        public async Task<AuthDataResult<UserInfoDto>> ChangePassword(string currentUserName, ChangePasswordDto changePasswordDto)
        {            
            var currentUser = await _userManager.FindByNameAsync(currentUserName);
            if (currentUser is null)
            {                
                return new AuthDataResult<UserInfoDto>() { IsSuccess = false, Message = "Kullanıcı bilgileri getirilirken bir hata oluştu..." };
            }
            var result = await _userManager.ChangePasswordAsync(currentUser!, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                return new AuthDataResult<UserInfoDto>() { IsSuccess = false, Message = "Şifre değiştirme aşamasında bir hata oluştu..." };
            }            
            UserInfoDto userInfo = await CreateAppUserInfo(currentUser, changePasswordDto.NewPassword);
            return new AuthDataResult<UserInfoDto>() { IsSuccess = true, Message = "Şifre değiştirme işlemi başarılı", Data = userInfo };
        }

        public async Task<UserInfoDto> CreateAppUserInfo(AuthUser user, string password)
        {
            IList<string> roleNames = await _userManager.GetRolesAsync(user!);
            string roleName = roleNames.First();
            if (roleName is null)
            {
                return new UserInfoDto();
            }
            return new UserInfoDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                RoleName = roleName,
                Password = password,
            };
        }
                
        public async Task<AuthDataResult<AuthUser>> LoginCheck(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null)
            {
                return new AuthDataResult<AuthUser>() { IsSuccess = false, Message = "Email adresiniz veya şifreniz hatalı!" };
            }
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result)
            {
                return new AuthDataResult<AuthUser>() { IsSuccess = false, Message = "Email adresiniz veya şifreniz hatalı!" };
            }            
            return new AuthDataResult<AuthUser>() { IsSuccess = true, Message = "Giriş işlemi başarılı", Data = user };
        }

        public async Task<AuthDataResult<UserInfoDto>> LoginResponse(AuthUser currentUser, string password )
        {
            var responseData = await CreateAppUserInfo(currentUser, password);
            if (responseData is null)
            {
                return new AuthDataResult<UserInfoDto>() { IsSuccess = false, Message = "Beklenmedik bir hata oluştur" };
            }
            return new AuthDataResult<UserInfoDto>() { IsSuccess = true, Message = "İşlem başarılı", Data = responseData };
        }

        public async Task<AuthDataResult<UserInfoDto>> Register(RegisterDto registerDto)
        {
            bool isEmailExists = await _userManager.Users.AnyAsync(u => u.NormalizedEmail == registerDto.Email.ToUpper(CultureInfo.GetCultureInfo("en-US")));
            if (isEmailExists)
            {
                return new AuthDataResult<UserInfoDto>() { IsSuccess = false, Message = "Mail adresi sistemde kayıtlı!" };
            }
            AuthUser newUser = _mapper.Map(registerDto, new AuthUser());
            var result = await _userManager.CreateAsync(newUser, registerDto.Password);
            if (!result.Succeeded)
            {
                return new AuthDataResult<UserInfoDto>() { IsSuccess = false, Message = "Yeni kullanıcı oluşturma aşamasında bir hata oluştu..." };
            }
            result = await _userManager.AddToRoleAsync(newUser, "User")!;
            if (!result.Succeeded)
            {
                return new AuthDataResult<UserInfoDto>() { IsSuccess = false, Message = "Yeni kullanıcı için rol ekleme aşamasında bir hata oluştu..." };
            }       
            UserInfoDto userInfo = await CreateAppUserInfo(newUser, registerDto.Password);            
            return new AuthDataResult<UserInfoDto>() { IsSuccess = true, Message = "Kayıt işlemi başarılı", Data = userInfo };
        }
        #endregion
    }
}
