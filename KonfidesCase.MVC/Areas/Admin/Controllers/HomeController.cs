using AutoMapper;
using KonfidesCase.MVC.Areas.Admin.ViewModels;
using KonfidesCase.MVC.BusinessLogic.Services;
using KonfidesCase.MVC.Models;
using KonfidesCase.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        public HomeController(IHttpClientFactory httpClientFactory, ILogger<HomeController> logger, IApiService apiService, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _apiService = apiService;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }
        #endregion
        
        #region Index Action       
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {            
            var tempData = TempData["IndexData"];
            if (tempData is null)
            {
                var resultApi = await _apiService.ApiGetResponse("url", "Home", "get-current-user");
                if (string.IsNullOrEmpty(resultApi))
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }                
                var sessionData = _contextAccessor.HttpContext!.Session.GetString(resultApi);
                DataResult<UserInfoVM> userInfoSession = JsonConvert.DeserializeObject<DataResult<UserInfoVM>>(sessionData!)!;
                return View(userInfoSession);
            }
            DataResult<UserInfoVM> userInfo = JsonConvert.DeserializeObject<DataResult<UserInfoVM>>((string)tempData!)!;
            return View(userInfo);
        }
        #endregion
                
        #region Logout Action
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            var resultApi = await _apiService.ApiGetResponse("url", "Home", "logout");
            DataResult<string> responseData = JsonConvert.DeserializeObject<DataResult<string>>(resultApi)!;            
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
        public async Task<IActionResult> CreateCategory(CreateCategoryVM createCategoryVM)
        {
            ViewData["CreateCategoryMessage"] = null;
            var resultApi = await _apiService.ApiPostResponse(createCategoryVM, "admin-url", "Admin", "create-category");
            DataResult<CategoryVM> responseData = JsonConvert.DeserializeObject<DataResult<CategoryVM>>(resultApi)!;
            if (!responseData.IsSuccess)
            {
                ViewData["CreateCategoryMessage"] = responseData.Message;
                return View();
            }
            return RedirectToAction("GetCategories", "Home", new { area = "admin" });
        }
        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            ViewData["GetCategoryMessage"] = null;
            var resultApi = await _apiService.ApiGetResponse("url", "Home", "get-categories");
            DataResult<ICollection<CategoryVM>> responseData = JsonConvert.DeserializeObject<DataResult<ICollection<CategoryVM>>>(resultApi)!;
            if (!responseData.IsSuccess)
            {
                ViewData["GetCategoryMessage"] = responseData.Message;
                return View();
            }
            return View(responseData.Data);
        }

        [HttpGet("UpdateCategory")]
        public IActionResult UpdateCategory([FromQuery] int categoryId)
        {
            return View(new CategoryVM() { Id = categoryId });
        }
        [HttpPost("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryVM updateCategoryVM)
        {
            ViewData["UpdateCategoryMessage"] = null;
            var resultApi = await _apiService.ApiPutResponse(updateCategoryVM, "admin-url", "Admin", "update-category");
            DataResult<CategoryVM> responseData = JsonConvert.DeserializeObject<DataResult<CategoryVM>>(resultApi)!;
            if (!responseData.IsSuccess)
            {
                ViewData["UpdateCategoryMessage"] = responseData.Message;
                return View();
            }            
            return RedirectToAction("GetCategories", "Home", new { area = "admin" });
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
            DataResult<CategoryVM> responseData = JsonConvert.DeserializeObject<DataResult<CategoryVM>>(resultApi)!;
            if (!responseData.IsSuccess)
            {
                ViewData["CreateCityMessage"] = responseData.Message;
                return View();
            }
            return RedirectToAction("Index", "Home", new { area = "admin" });
        }
        [HttpGet("GetCities")]
        public async Task<IActionResult> GetCities()
        {
            ViewData["GetCityMessage"] = null;
            var resultApi = await _apiService.ApiGetResponse("url", "Home", "get-cities");
            DataResult<ICollection<CityVM>> responseData = JsonConvert.DeserializeObject<DataResult<ICollection<CityVM>>>(resultApi)!;
            if (!responseData.IsSuccess)
            {
                ViewData["GetCityMessage"] = responseData.Message;
                return View();
            }
            return View(responseData.Data);
        }
        [HttpGet("UpdateCity")]        
        public IActionResult UpdateCity([FromQuery(Name ="id")]int cityId)
        {                   
            return View(new CityVM() { Id = cityId });
        }
        [HttpPost("UpdateCity")]
        public async Task<IActionResult> UpdateCity(CityVM updateCityVM)
        {
            ViewData["UpdateCityMessage"] = null;
            var resultApi = await _apiService.ApiPutResponse(updateCityVM, "admin-url", "Admin", "update-city");
            DataResult< CityVM > responseData = JsonConvert.DeserializeObject<DataResult<CityVM>>(resultApi)!;
            if (!responseData.IsSuccess)
            {
                ViewData["UpdateCityMessage"] = responseData.Message;
                return View();
            }
            return RedirectToAction("GetCities", "Home", new { area = "admin" });
        }
        #endregion

        #region ActivityActions
        [HttpGet("GetActivities")]
        public async Task<IActionResult> GetActivities()
        {
            var resultCategories = await _apiService.ApiGetResponse("url", "Home", "get-categories");
            DataResult<ICollection<CategoryVM>> responseCategories = JsonConvert.DeserializeObject<DataResult<ICollection<CategoryVM>>>(resultCategories)!;
            var resultCities = await _apiService.ApiGetResponse("url", "Home", "get-cities");
            DataResult<ICollection<CityVM>> responseCities = JsonConvert.DeserializeObject<DataResult<ICollection<CityVM>>>(resultCities)!;

            var resultApi = await _apiService.ApiGetResponse("url", "Home", "get-activities");
            DataResult<ICollection<ActivityVM>> activities = JsonConvert.DeserializeObject<DataResult<ICollection<ActivityVM>>>(resultApi)!;

            List<ConfirmActivityVM> activityList = new();
            foreach (ActivityVM activity in activities.Data!)
            {
                ConfirmActivityVM activityDetailVM = new();
                _mapper.Map(activity, activityDetailVM);
                activityDetailVM.Category = responseCategories.Data!.First(c => c.Id == activity.CategoryId).Name;
                activityDetailVM.City = responseCities.Data!.First(c => c.Id == activity.CityId).Name;
                activityList.Add(activityDetailVM);
            }
            return View(activityList);
        }
        [HttpGet("ConfirmActivity")]
        public async Task<IActionResult> ConfirmActivity(Guid activityId)
        {
            var resultApi = await _apiService.ApiPostResponse(activityId, "admin-url", "Home", "update-category");
            DataResult<ActivityVM> responseData = JsonConvert.DeserializeObject<DataResult<ActivityVM>>(resultApi)!;
            if (!responseData.IsSuccess)
            {
                ViewData["ConfirmActivityMessage"] = responseData.Message;
                return View();
            }
            return View(responseData.Data);            
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmActivity(ConfirmActivityVM confirmActivityVM)
        {
            ViewData["ConfirmActivityMessage"] = null;
            var resultApi = await _apiService.ApiPutResponse(confirmActivityVM, "admin-url", "Admin", "confirm-activity");
            DataResult<ActivityVM> responseData = JsonConvert.DeserializeObject<DataResult<ActivityVM>>(resultApi)!;
            if (!responseData.IsSuccess)
            {
                ViewData["ConfirmActivityMessage"] = responseData.Message;
                return View();
            }
            return RedirectToAction("GetActivities", "Home", new { area = "admin" });
        }
        #endregion

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
