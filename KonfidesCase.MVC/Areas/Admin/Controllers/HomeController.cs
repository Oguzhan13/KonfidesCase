using KonfidesCase.MVC.Models;
using KonfidesCase.ViewModel;
using KonfidesCase.ViewModel.Admin;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace KonfidesCase.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class HomeController : Controller
    {
        #region Fields & Constructor
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        #endregion

        #region Actions
        [HttpGet("index")]
        public IActionResult Index(UserInfo userInfo)
        {
            if (string.IsNullOrEmpty(userInfo.Email))
            {
                var tempData = TempData["IndexData"];
                if (tempData is null)
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                userInfo = JsonConvert.DeserializeObject<UserInfo>((string)tempData)!;                
            }
            return View(userInfo);
        }

        [HttpGet("change-password")]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var jsonPassword = JsonConvert.SerializeObject(changePasswordVM);
            HttpClient client = _httpClientFactory.CreateClient("url");
            var data = new StringContent(jsonPassword, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("Home/change-password", data);
            var result = await response.Content.ReadAsStringAsync();
            DataResult<UserInfo> responseChangePassword = JsonConvert.DeserializeObject<DataResult<UserInfo>>(result)!;

            return RedirectToAction(nameof(Index), responseChangePassword.Data);
        }

        [HttpGet("create-category")]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost("create-category")]
        public async Task<IActionResult> CreateCategory(NewCategoryVM newCategoryVM)
        {
            var jsonPassword = JsonConvert.SerializeObject(newCategoryVM);
            HttpClient client = _httpClientFactory.CreateClient("admin-url");
            var data = new StringContent(jsonPassword, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Admin/create-category", data);
            var result = await response.Content.ReadAsStringAsync();
            return RedirectToAction(nameof(Index), "Home");
        }
        [HttpGet("get-categories")]
        public async Task<IActionResult> GetCategories()
        {
            HttpClient client = _httpClientFactory.CreateClient("url");
            var response = await client.GetAsync("Home/get-categories");
            var result = await response.Content.ReadAsStringAsync();
            DataResult<IList<CategoriesVM>> responseGetCategories = JsonConvert.DeserializeObject<DataResult<IList<CategoriesVM>>>(result)!;            
            return View(responseGetCategories.Data);
        }
        #endregion
    }
}
