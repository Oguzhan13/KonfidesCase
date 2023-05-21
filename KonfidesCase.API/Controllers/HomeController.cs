using KonfidesCase.Authentication.BusinessLogic.Services;
using KonfidesCase.Authentication.Dtos;
using KonfidesCase.Authentication.Entities;
using KonfidesCase.Authentication.Utilities;
using KonfidesCase.BLL.Services.Concretes;
using KonfidesCase.BLL.Services.Interfaces;
using KonfidesCase.BLL.Utilities;
using KonfidesCase.DTO.Activity;
using KonfidesCase.DTO.Category;
using KonfidesCase.DTO.City;
using KonfidesCase.DTO.Ticket;
using KonfidesCase.Entity.Entities;
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
        private readonly IAdminService _adminService;
        public HomeController(IAuthService authService, IHomeService homeService, SignInManager<AuthUser> signInManager, IHttpContextAccessor httpContextAccessor, IAdminService adminService)
        {
            _authService = authService;
            _homeService = homeService;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _adminService = adminService;
        }
        #endregion

        #region Helper Methods
        [HttpGet("has-current-user")]
        public string HasCurrentUser()
        {
            return _httpContextAccessor.HttpContext!.User.Identity!.Name!;
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
            string currentUserName = HasCurrentUser();
            return string.IsNullOrEmpty(currentUserName) ? BadRequest(new DataResult<string>() { IsSuccess = false, Message = "Oturum süreniz sona erdi. Lütfen tekrar giriş yapınız" }) : Ok(new DataResult<string>() { IsSuccess = true, Message = "Aktif kullanıcı bulundu", Data = currentUserName });

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
            string currentUserName = HasCurrentUser();
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

        #region Logout Action
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            string currentUserName = HasCurrentUser();
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
            string currentUserName = HasCurrentUser();
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
            string currentUserName = HasCurrentUser();
            if (string.IsNullOrEmpty(currentUserName))
            {
                DataResult<ICollection<Category>> errorResponse = new() { IsSuccess = false, Message = "Aktif kullanıcı bulunamadı" };
                return BadRequest(errorResponse);
            }
            var response = await _homeService.GetCategories(currentUserName);
            return response.IsSuccess ? Ok(response) : NotFound(response);
        }        
        #endregion

        #region City Action
        [HttpGet("get-cities")]
        public async Task<IActionResult> GetCities()
        {
            string currentUserName = HasCurrentUser();
            if (string.IsNullOrEmpty(currentUserName))
            {
                DataResult<ICollection<City>> errorResponse = new() { IsSuccess = false, Message = "Aktif kullanıcı bulunamadı" };
                return BadRequest(errorResponse);
            }
            var response = await _homeService.GetCities(currentUserName);
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
            string currentUserName = HasCurrentUser();
            if (string.IsNullOrEmpty(currentUserName))
            {
                DataResult<Activity> errorResponse = new() { IsSuccess = false, Message = "Aktif kullanıcı bulunamadı" };
                return BadRequest(errorResponse);
            }
            var response = await _homeService.CreateActivity(createActivityDto, currentUserName);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        [HttpGet("get-activities")]
        public async Task<IActionResult> GetActivities()
        {
            string currentUserName = HasCurrentUser();
            if (string.IsNullOrEmpty(currentUserName))
            {
                DataResult<ICollection<Activity>> errorResponse = new() { IsSuccess = false, Message = "Aktif kullanıcı bulunamadı" };
                return BadRequest(errorResponse);
            }
            var response = await _homeService.GetActivities(currentUserName);
            return response.IsSuccess ? Ok(response) : NotFound(response);
        }
        [HttpPut("update-activity")]
        public async Task<IActionResult> UpdateActivity(UpdateActivityDto updateActivityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string currentUserName = HasCurrentUser();
            if (string.IsNullOrEmpty(currentUserName))
            {
                DataResult<Activity> errorResponse = new() { IsSuccess = false, Message = "Aktif kullanıcı bulunamadı" };
                return BadRequest(errorResponse);
            }
            var response = await _homeService.UpdateActivity(updateActivityDto, currentUserName);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        [HttpPost("cancel-activity")]
        public async Task<IActionResult> CancelActivity(CancelActivityDto cancelActivityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string currentUserName = HasCurrentUser();
            if (string.IsNullOrEmpty(currentUserName))
            {
                DataResult<Activity> errorResponse = new() { IsSuccess = false, Message = "Aktif kullanıcı bulunamadı" };
                return BadRequest(errorResponse);
            }
            var response = await _homeService.CancelActivity(cancelActivityDto, currentUserName);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        [HttpGet("created-activities")]
        public async Task<IActionResult> GetMyCreatedActivities()
        {
            string currentUserName = HasCurrentUser();
            if (string.IsNullOrEmpty(currentUserName))
            {
                DataResult<ICollection<Activity>> errorResponse = new() { IsSuccess = false, Message = "Aktif kullanıcı bulunamadı" };
                return BadRequest(errorResponse);
            }
            var response = await _homeService.GetMyCreatedActivities(currentUserName);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        [HttpGet("attended-activities")]
        public async Task<IActionResult> GetAttendedActivities()
        {
            string currentUserName = HasCurrentUser();
            if (string.IsNullOrEmpty(currentUserName))
            {
                DataResult<ICollection<Activity>> errorResponse = new() { IsSuccess = false, Message = "Aktif kullanıcı bulunamadı" };
                return BadRequest(errorResponse);
            }
            var response = await _homeService.GetAttendedActivities(currentUserName);
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
            string currentUserName = HasCurrentUser();
            if (string.IsNullOrEmpty(currentUserName))
            {
                DataResult<Activity> errorResponse = new() { IsSuccess = false, Message = "Aktif kullanıcı bulunamadı" };
                return BadRequest(errorResponse);
            }
            var response = await _homeService.BuyTicket(createTicketDto, currentUserName);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        #endregion

        #region Admin Actions
        
        #region Category Actions
        [HttpPost("create-category")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string currentUserName = HasCurrentUser();
            var response = await _adminService.CreateCategory(createCategoryDto, currentUserName);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        [HttpPut("update-category")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string currentUserName = HasCurrentUser();
            var response = await _adminService.UpdateCategory(updateCategoryDto, currentUserName);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        #endregion

        #region City Actions
        [HttpPost("create-city")]
        public async Task<IActionResult> CreateCity(CreateCityDto createCityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string currentUserName = HasCurrentUser();
            var response = await _adminService.CreateCity(createCityDto, currentUserName);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut("update-city")]
        public async Task<IActionResult> UpdateCity(UpdateCityDto updateCityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string currentUserName = HasCurrentUser();
            var response = await _adminService.UpdateCity(updateCityDto, currentUserName);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        #endregion

        #region Activity Action
        [HttpPut("confirm-activity")]
        public async Task<IActionResult> ConfirmActivity(ConfirmActivityDto confirmActivityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string currentUserName = HasCurrentUser();
            var response = await _adminService.ConfirmActivity(confirmActivityDto, currentUserName);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        #endregion
        
        #endregion
    }
}
