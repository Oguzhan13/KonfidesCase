using KonfidesCase.MVC.Areas.User.Models;
using KonfidesCase.MVC.BusinessLogic.Services;
using KonfidesCase.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KonfidesCase.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class HomeController : Controller
    {
        #region Fields & Constructor
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IApiService _apiService;
        public HomeController(IHttpClientFactory httpClientFactory, ILogger<HomeController> logger, IApiService apiService)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _apiService = apiService;
        }
        #endregion

        #region Index Action
        [HttpGet("Index")]
        public IActionResult Index(DataResult<UserInfo> userInfo)
        {
            if (userInfo.Data is null)
            {
                var tempData = TempData["IndexData"];
                if (tempData is null)
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                userInfo = JsonConvert.DeserializeObject<DataResult<UserInfo>>((string)tempData)!;
            }
            return View(userInfo);
        }
        #endregion

        #region Logout Action
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            var resultApi = await _apiService.ApiGetResponse("url", "Home", "logout");
            _apiService.ApiDeserializeResult(resultApi, out DataResult<string> responseData);
            TempData["LogoutData"] = JsonConvert.SerializeObject(responseData);
            return RedirectToAction("Login", "Home");
        }
        #endregion

        #region Category Actions
        [HttpGet("CreateCategory")]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory(string categoryName)
        {
            ViewData["CreateCategoryMessage"] = null;
            var resultApi = await _apiService.ApiPostResponse(categoryName, "admin-url", "Home", "create-category");
            DataResult<Category> responseData = JsonConvert.DeserializeObject<DataResult<Category>>(resultApi)!;
            if (!responseData.IsSuccess)
            {
                ViewData["CreateCategoryMessage"] = responseData.Message;
                return View();
            }
            return RedirectToAction("Index", "Home", new { area = "admin" });
        }

        [HttpGet("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory()
        {
            return View();
        }
        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory()
        {

            return RedirectToAction("Index", "Home", new { area = "admin" });
        }
        #endregion

        #region City Actions
        [HttpGet("CreateCity")]
        public IActionResult CreateCity()
        {
            return View();
        }
        [HttpPost("CreateCity")]
        public async Task<IActionResult> CreateCity(string categoryName)
        {
            ViewData["CreateCityMessage"] = null;
            var resultApi = await _apiService.ApiPostResponse(categoryName, "admin-url", "Home", "create-city");
            DataResult<Category> responseData = JsonConvert.DeserializeObject<DataResult<Category>>(resultApi)!;
            if (!responseData.IsSuccess)
            {
                ViewData["CreateCityMessage"] = responseData.Message;
                return View();
            }
            return RedirectToAction("Index", "Home", new { area = "admin" });
        }

        #endregion
        //[HttpGet("change-password")]
        //public IActionResult ChangePassword()
        //{
        //    return View();
        //}
        //[HttpPost("change-password")]
        //public async Task<IActionResult> ChangePassword(ChangePasswordVM changePasswordVM)
        //{
        //    var jsonPassword = JsonConvert.SerializeObject(changePasswordVM);
        //    HttpClient client = _httpClientFactory.CreateClient("url");
        //    var data = new StringContent(jsonPassword, Encoding.UTF8, "application/json");
        //    var response = await client.PutAsync("Home/change-password", data);
        //    var result = await response.Content.ReadAsStringAsync();
        //    DataResult<UserInfo> responseChangePassword = JsonConvert.DeserializeObject<DataResult<UserInfo>>(result)!;

        //    return RedirectToAction(nameof(Index), responseChangePassword.Data);
        //}


        //[HttpGet("create-category")]
        //public IActionResult CreateCategory()
        //{
        //    return View();
        //}
        //[HttpPost("create-category")]
        //public async Task<IActionResult> CreateCategory(NewCategoryVM newCategoryVM)
        //{
        //    var jsonPassword = JsonConvert.SerializeObject(newCategoryVM);
        //    HttpClient client = _httpClientFactory.CreateClient("admin-url");
        //    var data = new StringContent(jsonPassword, Encoding.UTF8, "application/json");
        //    var response = await client.PostAsync("Admin/create-category", data);
        //    var result = await response.Content.ReadAsStringAsync();
        //    return RedirectToAction(nameof(Index), "Home");
        //}
        //[HttpGet("get-categories")]
        //public async Task<IActionResult> GetCategories()
        //{
        //    HttpClient client = _httpClientFactory.CreateClient("url");
        //    var response = await client.GetAsync("Home/get-categories");
        //    var result = await response.Content.ReadAsStringAsync();
        //    DataResult<IList<CategoriesVM>> responseGetCategories = JsonConvert.DeserializeObject<DataResult<IList<CategoriesVM>>>(result)!;            
        //    return View(responseGetCategories.Data);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
