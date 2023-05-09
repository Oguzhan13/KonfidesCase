using KonfidesCase.BLL.Services.Interfaces;
using KonfidesCase.DTO.Category;
using KonfidesCase.DTO.City;
using Microsoft.AspNetCore.Mvc;

namespace KonfidesCase.API.Controllers
{
    [Route("admin/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        #region Fields & Constructor
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        #endregion

        #region Category Actions
        [HttpPost("create-category")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _adminService.CreateCategory(createCategoryDto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        [HttpPost("get-category")]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            var response = await _adminService.GetCategory(categoryId);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        [HttpPut("update-category")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _adminService.UpdateCategory(updateCategoryDto);
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
            var response = await _adminService.CreateCity(createCityDto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        [HttpPost("get-city")]
        public async Task<IActionResult> GetCity(int cityId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _adminService.GetCity(cityId);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        [HttpPut("update-city")]
        public async Task<IActionResult> UpdateCity(UpdateCityDto updateCityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _adminService.UpdateCity(updateCityDto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        #endregion

        #region Activity Action
        [HttpPut("confirm-activity")]
        public async Task<IActionResult> ConfirmActivity(Guid activityId, bool confirmActivity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _adminService.ConfirmActivity(activityId, confirmActivity);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        #endregion
    }
}
