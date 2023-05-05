using Microsoft.AspNetCore.Http;

namespace KonfidesCase.Authentication.BusinessLogic.Services
{
    public class AuthService : IAuthService
    {
        #region Fields & Constructor
        private readonly UserManager<AuthUser> _userManager;
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;
        public AuthService(UserManager<AuthUser> userManager, SignInManager<AuthUser> signInManager, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<AuthDataResult<RegisterDto>> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            string currentUserName = _contextAccessor.HttpContext!.User.Identity!.Name!;
            if (string.IsNullOrEmpty(currentUserName))
            {
                return new AuthDataResult<RegisterDto>() { IsSuccess = false, Message = "Aktif kullanıcı bulunamadı" };
            }
            var currentUser = await _userManager.FindByNameAsync(currentUserName);
            if (currentUser is null)
            {
                return new AuthDataResult<RegisterDto>() { IsSuccess = false, Message = "Kullanıcı bilgileri getirilirken bir hata oluştu..." };
            }
            var result = await _userManager.ChangePasswordAsync(currentUser!, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                return new AuthDataResult<RegisterDto>() { IsSuccess = false, Message = "Şifre değiştirme aşamasında bir hata oluştu..." };
            }
            RegisterDto registerDto = _mapper.Map(currentUser, new RegisterDto());
            registerDto.Password = changePasswordDto.NewPassword;
            return new AuthDataResult<RegisterDto>() { IsSuccess = true, Message = "Şifre değiştirme işlemi başarılı", Data = registerDto };
        }

        public async Task<AuthDataResult<RegisterDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null)
            {
                return new AuthDataResult<RegisterDto>() { IsSuccess = false, Message = "Aktif kullanıcı bulunamadı!" };
            }
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result)
            {
                return new AuthDataResult<RegisterDto>() { IsSuccess = false, Message = "Email adresiniz veya şifreniz hatalı!" };
            }
            await _signInManager.SignInAsync(user, true);

            RegisterDto registerDto = _mapper.Map(user, new RegisterDto());
            registerDto.Password = loginDto.Password;
            return new AuthDataResult<RegisterDto>() { IsSuccess = true, Message = "Giriş işlemi başarılı", Data = registerDto };
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<AuthDataResult<AuthUser>> Register(RegisterDto registerDto)
        {
            bool isEmailExists = await _userManager.Users.AnyAsync(u => u.NormalizedEmail == registerDto.Email.ToUpper(CultureInfo.GetCultureInfo("en-US")));
            if (isEmailExists)
            {
                return new AuthDataResult<AuthUser>() { IsSuccess = false, Message = "Mail adresi sistemde kayıtlı!" };
            }
            AuthUser newUser = _mapper.Map(registerDto, new AuthUser());
            var result = await _userManager.CreateAsync(newUser, registerDto.Password);
            if (!result.Succeeded)
            {
                return new AuthDataResult<AuthUser>() { IsSuccess = false, Message = "Yeni kullanıcı oluşturma aşamasında bir hata oluştu..." };
            }
            result = await _userManager.AddToRoleAsync(newUser, "User")!;
            if (!result.Succeeded)
            {
                return new AuthDataResult<AuthUser>() { IsSuccess = false, Message = "Yeni kullanıcı için rol ekleme aşamasında bir hata oluştu..." };
            }
            return new AuthDataResult<AuthUser>() { IsSuccess = true, Message = "Kayıt işlemi başarılı", Data = newUser };
        }
        #endregion
    }
}
