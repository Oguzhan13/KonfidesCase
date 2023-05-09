using AutoMapper;
using KonfidesCase.MVC.Areas.User.Models;
using KonfidesCase.MVC.Areas.User.ViewModels;
using KonfidesCase.MVC.BusinessLogic.Services;
using KonfidesCase.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KonfidesCase.MVC.Areas.User.Controllers
{
    [Area("User")]
    [Route("user/[controller]")]
    public class HomeController : Controller
    {
        #region Fields & Constructor
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        public HomeController(IHttpClientFactory httpClientFactory, IApiService apiService, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _apiService = apiService;
            _mapper = mapper;
        }
        #endregion

        
        internal static object indexData = new();

        #region Index Action       
        [HttpGet("Index")]
        public IActionResult Index()
        {
            indexData = TempData["IndexData"] != null ? TempData["IndexData"]! : indexData!;
            if (indexData is null)
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }            
            DataResult<UserInfo>userInfo = JsonConvert.DeserializeObject<DataResult<UserInfo>>((string)indexData)!;
            return View(userInfo);
        }
        #endregion

        #region ChangePassword Action
        [HttpGet("ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var resultApi = await _apiService.ApiPutResponse(changePasswordVM, "url", "Home", "change-password");
            DataResult<UserInfo> responseData = JsonConvert.DeserializeObject<DataResult<UserInfo>>(resultApi)!;
            return RedirectToAction("Index", "Home", new { area = "user" });
        }
        #endregion

        #region Logout Action
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            var resultApi = await _apiService.ApiGetResponse("url", "Home", "logout");
            _apiService.ApiDeserializeResult(resultApi, out DataResult<string> responseData);
            TempData["LogoutData"] = JsonConvert.SerializeObject(responseData);
            indexData = null!;
            return RedirectToAction("Login", "Home");
        }
        #endregion

        #region Activity Actions
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

        [HttpGet("GetActivities")]
        public async Task<IActionResult> GetActivities()
        {
            var resultCategories = await _apiService.ApiGetResponse("url", "Home", "get-categories");
            DataResult<ICollection<Category>> responseCategories = JsonConvert.DeserializeObject<DataResult<ICollection<Category>>>(resultCategories)!;
            var resultCities = await _apiService.ApiGetResponse("url", "Home", "get-cities");
            DataResult<ICollection<City>> responseCities = JsonConvert.DeserializeObject<DataResult<ICollection<City>>>(resultCities)!;

            var resultApi = await _apiService.ApiGetResponse("url", "Home", "get-activities");
            DataResult<ICollection<Activity>> activities = JsonConvert.DeserializeObject<DataResult<ICollection<Activity>>>(resultApi)!;

            List<ActivityDetailVM> activityList = new();
            foreach (Activity activity in activities.Data!)
            {
                ActivityDetailVM activityDetailVM = new();
                _mapper.Map(activity, activityDetailVM);
                activityDetailVM.Category = responseCategories.Data!.First(c => c.Id == activity.CategoryId).Name;
                activityDetailVM.City = responseCities.Data!.First(c => c.Id == activity.CityId).Name;
                activityList.Add(activityDetailVM);
            }            
            return View(activityList);
        }
        #endregion
    }
}
