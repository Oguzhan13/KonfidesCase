using KonfidesCase.MVC.Areas.User.Models;
using KonfidesCase.MVC.Areas.User.ViewModels;
using KonfidesCase.MVC.BusinessLogic.Services;
using KonfidesCase.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Dynamic;

namespace KonfidesCase.MVC.Areas.User.Controllers
{
    [Area("User")]
    [Route("user/[controller]")]
    public class HomeController : Controller
    {
        #region Fields & Constructor
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IApiService _apiService;
        public HomeController(IHttpClientFactory httpClientFactory, IApiService apiService)
        {
            _httpClientFactory = httpClientFactory;
            _apiService = apiService;
        }
        #endregion

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

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            var resultApi = await _apiService.ApiGetResponse("url", "Home", "logout");
            _apiService.ApiDeserializeResult(resultApi, out DataResult<string> responseData);
            TempData["LogoutData"] = JsonConvert.SerializeObject(responseData);            
            return RedirectToAction("Login", "Home");
        }

        [HttpGet("CreateActivity")]
        public async Task<IActionResult> CreateActivity()
        {            
            var resultCategories = await _apiService.ApiGetResponse("url", "Home", "get-categories");
            DataResult<ICollection<Category>> responseCategories = JsonConvert.DeserializeObject<DataResult<ICollection<Category>>>(resultCategories)!;            
            var resultCities = await _apiService.ApiGetResponse("url", "Home", "get-cities");
            DataResult<ICollection<City>> responseCities = JsonConvert.DeserializeObject<DataResult<ICollection<City>>>(resultCities)!;

            return View(new CreateActivityVM()
            {
                Categories = responseCategories.Data!,
                Cities = responseCities.Data!,
            });
        }
        [HttpPost("CreateActivity")]
        public async Task<IActionResult> CreateActivity(CreateActivityVM createActivityVM)
        {
            ViewData["CreateActivityMessage"] = null;
            var resultApi = await _apiService.ApiPostResponse(createActivityVM, "url", "Home", "create-activity");
            DataResult<Activity> responseData = JsonConvert.DeserializeObject<DataResult<Activity>>(resultApi)!;
            if (!responseData.IsSuccess)
            {
                ViewData["CreateActivityMessage"] = responseData.Message;
                return View();
            }
            return RedirectToAction("Index", "Home", new { area = "user" });
        }
    }
}
