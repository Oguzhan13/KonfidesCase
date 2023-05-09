using AutoMapper;
using KonfidesCase.MVC.Areas.Admin.Models;
using KonfidesCase.MVC.Areas.Admin.ViewModels;
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
        private readonly IMapper _mapper;
        public HomeController(IHttpClientFactory httpClientFactory, ILogger<HomeController> logger, IApiService apiService, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
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
            DataResult<UserInfo> userInfo = JsonConvert.DeserializeObject<DataResult<UserInfo>>((string)indexData)!;
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

        //[HttpGet("UpdateCategory")]
        //public async Task<IActionResult> UpdateCategory()
        //{
        //    return View();
        //}
        //[HttpPut("UpdateCategory")]
        //public async Task<IActionResult> UpdateCategory()
        //{

        //    return RedirectToAction("Index", "Home", new { area = "admin" });
        //}
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

        #region ActivityActions
        [HttpGet("GetActivities")]
        public async Task<IActionResult> GetActivities()
        {
            var resultCategories = await _apiService.ApiGetResponse("url", "Home", "get-categories");
            DataResult<ICollection<Category>> responseCategories = JsonConvert.DeserializeObject<DataResult<ICollection<Category>>>(resultCategories)!;
            var resultCities = await _apiService.ApiGetResponse("url", "Home", "get-cities");
            DataResult<ICollection<City>> responseCities = JsonConvert.DeserializeObject<DataResult<ICollection<City>>>(resultCities)!;

            var resultApi = await _apiService.ApiGetResponse("url", "Home", "get-activities");
            DataResult<ICollection<Activity>> activities = JsonConvert.DeserializeObject<DataResult<ICollection<Activity>>>(resultApi)!;

            List<ConfirmActivityVM> activityList = new();
            foreach (Activity activity in activities.Data!)
            {
                ConfirmActivityVM activityDetailVM = new();
                _mapper.Map(activity, activityDetailVM);
                activityDetailVM.Category = responseCategories.Data!.First(c => c.Id == activity.CategoryId).Name;
                activityDetailVM.City = responseCities.Data!.First(c => c.Id == activity.CityId).Name;
                activityList.Add(activityDetailVM);
            }
            return View(activityList);
        }
        #endregion

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
