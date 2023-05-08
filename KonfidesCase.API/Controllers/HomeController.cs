using KonfidesCase.Authentication.BusinessLogic.Services;
using KonfidesCase.Authentication.Dtos;
using KonfidesCase.BLL.Services.Interfaces;
using KonfidesCase.DTO.Category;
using KonfidesCase.DTO.City;
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
        private readonly IAdminService _adminService;
        public HomeController(IAuthService authService, IHomeService homeService, IAdminService adminService)
        {
            _authService = authService;
            _homeService = homeService;
            _adminService = adminService;
        }
        #endregion

        #region Actions

        #region Actions for All Users
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _authService.Login(loginDto);
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

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();
            return Ok();
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var response = await _authService.ChangePassword(changePasswordDto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
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

        #region Actions for Admin role
        //[HttpPost("create-category")]
        //public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var response = await _adminService.CreateCategory(createCategoryDto);
        //    return response.IsSuccess ? Ok(response) : BadRequest(response);
        //}
        //[HttpPut("update-category")]
        //public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var response = await _adminService.UpdateCategory(updateCategoryDto);
        //    return response.IsSuccess ? Ok(response) : BadRequest(response);
        //}

        //[HttpPost("create-city")]
        //public async Task<IActionResult> CreateCity(CreateCityDto createCityDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var response = await _adminService.CreateCity(createCityDto);
        //    return response.IsSuccess ? Ok(response) : BadRequest(response);
        //}
        //[HttpPut("update-city")]
        //public async Task<IActionResult> UpdateCity(UpdateCityDto updateCityDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var response = await _adminService.UpdateCity(updateCityDto);
        //    return response.IsSuccess ? Ok(response) : BadRequest(response);
        //}
        #endregion

        #endregion
    }
}
