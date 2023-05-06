using KonfidesCase.Authentication.BusinessLogic.Services;
using KonfidesCase.Authentication.Dtos;
using KonfidesCase.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KonfidesCase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        #region Fields & Constructor
        private readonly IAuthService _authService;
        private readonly IUserService _userService;          
        public HomeController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;            
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
            await _userService.CreateAppUser(response.Data!);
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
        #endregion
    }
}
