using KonfidesCase.Authentication.BusinessLogic.Services;
using KonfidesCase.Authentication.Dtos;
using KonfidesCase.Authentication.Entities;
using KonfidesCase.Authentication.Utilities;
using KonfidesCase.BLL.Services.Interfaces;
using KonfidesCase.DTO.Activity;
using KonfidesCase.DTO.Ticket;
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

        #region Login Action                
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
            var response = await _authService.LoginResponse(result.Data!, loginDto.Password);
            return response.IsSuccess ? Ok(response) : BadRequest(response);            
        }
        #endregion

        #region GetCurrentUser Action
        [HttpGet("get-current-user")]
        public IActionResult GetCurrentUser()
        {
            string currentUserName = _httpContextAccessor.HttpContext!.User.Identity!.Name!;            
            return string.IsNullOrEmpty(currentUserName) ? BadRequest("Aktif kullanıcı bulunamadı") : Ok(currentUserName);

        }
        #endregion

        #region Register Action
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _authService.Register(registerDto);            
            var createAppUser = await _homeService.CreateAppUser(response.Data!);
            if (!createAppUser.IsSuccess)
            {
                response.Message = createAppUser.Message;
                return BadRequest(response);
            }
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        #endregion

        #region Logout Method
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            string currentUserName = _httpContextAccessor.HttpContext!.User.Identity!.Name!;            
            return Ok(new AuthDataResult<string>() { IsSuccess = true, Message = "Çıkış işlemi başarılı" });            
        }
        #endregion

        #region ChangePassword Action
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
        #endregion

        #region Category Action
        [HttpGet("get-categories")]
        public async Task<IActionResult> GetCategories()
        {
            var response = await _homeService.GetCategories();
            return response.IsSuccess ? Ok(response) : NotFound(response);
        }        
        #endregion

        #region City Action
        [HttpGet("get-cities")]
        public async Task<IActionResult> GetCities()
        {
            var response = await _homeService.GetCities();
            return response.IsSuccess ? Ok(response) : NotFound(response);
        }
        #endregion

        #region Activity Actions
        [HttpPost("create-activity")]
        public async Task<IActionResult> CreateActivity(CreateActivityDto createActivityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _homeService.CreateActivity(createActivityDto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        [HttpGet("get-activities")]
        public async Task<IActionResult> GetActivities()
        {
            var response = await _homeService.GetActivities();
            return response.IsSuccess ? Ok(response) : NotFound(response);
        }
        [HttpPut("update-activity")]
        public async Task<IActionResult> UpdateActivity(UpdateActivityDto updateActivityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _homeService.UpdateActivity(updateActivityDto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        [HttpPost("cancel-activity")]
        public async Task<IActionResult> CancelActivity(CancelActivityDto cancelActivityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _homeService.CancelActivity(cancelActivityDto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        [HttpGet("created-activities")]
        public async Task<IActionResult> GetMyCreatedActivities()
        {
            var response = await _homeService.GetMyCreatedActivities();
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        [HttpGet("attended-activities")]
        public async Task<IActionResult> GetAttendedActivities()
        {
            var response = await _homeService.GetAttendedActivities();
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        #endregion

        #region Ticket Action
        [HttpPost("buy-ticket")]
        public async Task<IActionResult> BuyTicket(CreateTicketDto createTicketDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _homeService.BuyTicket(createTicketDto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        #endregion
    }
}
