using KonfidesCase.Authentication.BusinessLogic.Services;
using KonfidesCase.Authentication.Dtos;
using KonfidesCase.Authentication.Entities;
using KonfidesCase.Authentication.Utilities;
using KonfidesCase.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KonfidesCase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        #region Fields & Constructor
        private readonly IAuthService _authService;
        private readonly IHomeService _homeService;         
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(IAuthService authService, IHomeService homeService, SignInManager<AuthUser> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _authService = authService;
            _homeService = homeService;            
            _signInManager = signInManager;            
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Actions
                
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authService.LoginCheck(loginDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            await _signInManager.SignInAsync(result.Data!, true);
            var x = _httpContextAccessor.HttpContext!.User.Identity!.Name!;
            var response = await _authService.LoginResponse(result.Data!, loginDto.Password);
            return response.IsSuccess ? Ok(response) : BadRequest(response);            
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _authService.Register(registerDto);            
            await _homeService.CreateAppUser(response.Data!);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string currentUserName = _httpContextAccessor.HttpContext!.User.Identity!.Name!;
            if (string.IsNullOrEmpty(currentUserName))
            {
                AuthDataResult<UserInfoDto> errorResponse = new() { IsSuccess = false, Message = "Aktif kullanıcı bulunamadı" };
                return BadRequest(errorResponse);
            }
            var response = await _authService.ChangePassword(currentUserName, changePasswordDto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            var currentUserName = _httpContextAccessor.HttpContext!.User.Identity!.Name;            
            return string.IsNullOrEmpty(currentUserName) ? 
                Ok(new AuthDataResult<string>() { IsSuccess = true, Message = "Çıkış işlemi başarılı" }) : 
                BadRequest(new AuthDataResult<string>() { IsSuccess = true, Message = "Çıkış işlemi başarısız!", Data = currentUserName });
        }

        [HttpGet("get-categories")]
        public async Task<IActionResult> GetCategories()
        {
            var response = await _homeService.GetCategories();
            return response.IsSuccess ? Ok(response) : NotFound(response);
        }

        [HttpGet("get-cities")]
        public async Task<IActionResult> GetCities()
        {
            var response = await _homeService.GetCities();
            return response.IsSuccess ? Ok(response) : NotFound(response);
        }

        #endregion
    }
}
