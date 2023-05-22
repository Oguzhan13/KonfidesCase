using KonfidesCase.MVC.Areas.Admin.ViewModels;
using System.Text;

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

        #region Helper Method
        public async Task<DataResult<string>> HasCurrentUser()
        {
            var hasCurrentUser = await _apiService.ApiGetResponse("url", "Home", "get-current-user");
            return JsonConvert.DeserializeObject<DataResult<string>>(hasCurrentUser)!;            
        }
        #endregion

        #region Index Action       
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {            
            DataResult<string> responseCurrentUser = await HasCurrentUser();
            if (!responseCurrentUser.IsSuccess)
            {
                TempData["LogoutData"] = JsonConvert.SerializeObject(responseCurrentUser.Message);
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            var sessionData = _contextAccessor.HttpContext!.Session.GetString(responseCurrentUser.Data!);
            DataResult<UserInfoVM> userInfoSession = JsonConvert.DeserializeObject<DataResult<UserInfoVM>>(sessionData!)!;
            return View(userInfoSession);            
        }
        #endregion
                
        #region Logout Action
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            var resultApi = await _apiService.ApiGetResponse("url", "Home", "logout");
            DataResult<string> responseData = JsonConvert.DeserializeObject<DataResult<string>>(resultApi)!;            
            TempData["LogoutData"] = JsonConvert.SerializeObject(responseData.Message);
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
            var resultApi = await _apiService.ApiPostResponse(createCategoryVM, "url", "Home", "create-category");            
            DataResult<string> responseCurrentUser = await HasCurrentUser();
            if (!responseCurrentUser.IsSuccess)
            {
                TempData["LogoutData"] = JsonConvert.SerializeObject(responseCurrentUser.Message);
                return RedirectToAction("Login", "Home", new { area = "" });
            }            
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
            DataResult<string> responseCurrentUser = await HasCurrentUser();
            if (!responseCurrentUser.IsSuccess)
            {
                TempData["LogoutData"] = JsonConvert.SerializeObject(responseCurrentUser.Message);
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            DataResult<ICollection<CategoryVM>> responseData = JsonConvert.DeserializeObject<DataResult<ICollection<CategoryVM>>>(resultApi)!;
            if (!responseData.IsSuccess)
            {
                ViewData["GetCategoryMessage"] = responseData.Message;
                return View();
            }
            return View(responseData.Data);
        }

        [HttpGet("UpdateCategory")]
        public IActionResult UpdateCategory([FromQuery(Name = "id")] int categoryId, [FromQuery(Name = "name")] string categoryName)
        {
            return View(new CategoryVM() { Id = categoryId, Name = categoryName });
        }
        [HttpPost("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryVM updateCategoryVM)
        {
            ViewData["UpdateCategoryMessage"] = null;
            var resultApi = await _apiService.ApiPutResponse(updateCategoryVM, "url", "Home", "update-category");
            DataResult<string> responseCurrentUser = await HasCurrentUser();
            if (!responseCurrentUser.IsSuccess)
            {
                TempData["LogoutData"] = JsonConvert.SerializeObject(responseCurrentUser.Message);
                return RedirectToAction("Login", "Home", new { area = "" });
            }
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
        public async Task<IActionResult> CreateCity(CreateCityVM createCityVM)
        {
            ViewData["CreateCityMessage"] = null;
            var resultApi = await _apiService.ApiPostResponse(createCityVM, "url", "Home", "create-city");
            DataResult<string> responseCurrentUser = await HasCurrentUser();
            if (!responseCurrentUser.IsSuccess)
            {
                TempData["LogoutData"] = JsonConvert.SerializeObject(responseCurrentUser.Message);
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            DataResult<CityVM> responseData = JsonConvert.DeserializeObject<DataResult<CityVM>>(resultApi)!;
            if (!responseData.IsSuccess)
            {
                ViewData["CreateCityMessage"] = responseData.Message;
                return View();
            }
            return RedirectToAction("GetCities", "Home", new { area = "admin" });
        }
        [HttpGet("GetCities")]
        public async Task<IActionResult> GetCities()
        {
            ViewData["GetCityMessage"] = null;
            var resultApi = await _apiService.ApiGetResponse("url", "Home", "get-cities");
            DataResult<string> responseCurrentUser = await HasCurrentUser();
            if (!responseCurrentUser.IsSuccess)
            {
                TempData["LogoutData"] = JsonConvert.SerializeObject(responseCurrentUser.Message);
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            DataResult<ICollection<CityVM>> responseData = JsonConvert.DeserializeObject<DataResult<ICollection<CityVM>>>(resultApi)!;
            if (!responseData.IsSuccess)
            {
                ViewData["GetCityMessage"] = responseData.Message;
                return View();
            }
            return View(responseData.Data);
        }
        [HttpGet("UpdateCity")]        
        public IActionResult UpdateCity([FromQuery(Name ="id")]int cityId, [FromQuery(Name = "name")] string cityName)
        {                   
            return View(new CityVM() { Id = cityId, Name = cityName });
        }
        [HttpPost("UpdateCity")]
        public async Task<IActionResult> UpdateCity(CityVM updateCityVM)
        {
            ViewData["UpdateCityMessage"] = null;
            var resultApi = await _apiService.ApiPutResponse(updateCityVM, "url", "Home", "update-city");
            DataResult<string> responseCurrentUser = await HasCurrentUser();
            if (!responseCurrentUser.IsSuccess)
            {
                TempData["LogoutData"] = JsonConvert.SerializeObject(responseCurrentUser.Message);
                return RedirectToAction("Login", "Home", new { area = "" });
            }
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
            var resultCities = await _apiService.ApiGetResponse("url", "Home", "get-cities");
            var resultApi = await _apiService.ApiGetResponse("url", "Home", "get-activities");            
            DataResult<string> responseCurrentUser = await HasCurrentUser();
            if (!responseCurrentUser.IsSuccess)
            {
                TempData["LogoutData"] = JsonConvert.SerializeObject(responseCurrentUser.Message);
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            DataResult<ICollection<CategoryVM>> responseCategories = JsonConvert.DeserializeObject<DataResult<ICollection<CategoryVM>>>(resultCategories)!;
            DataResult<ICollection<CityVM>> responseCities = JsonConvert.DeserializeObject<DataResult<ICollection<CityVM>>>(resultCities)!;

            DataResult<ICollection<ActivityVM>> activities = JsonConvert.DeserializeObject<DataResult<ICollection<ActivityVM>>>(resultApi)!;

            List<ActivityDetailVM> activityList = new();
            foreach (ActivityVM activity in activities.Data!)
            {
                ActivityDetailVM activityDetailVM = new();
                _mapper.Map(activity, activityDetailVM);
                activityDetailVM.Category = responseCategories.Data!.First(c => c.Id == activity.CategoryId).Name;
                activityDetailVM.City = responseCities.Data!.First(c => c.Id == activity.CityId).Name;
                activityList.Add(activityDetailVM);
            }
            return View(activityList);
        }
        [HttpGet("ConfirmActivity")]
        public IActionResult ConfirmActivity([FromQuery(Name = "id")] Guid id, [FromQuery(Name = "organizer")] string organizer, [FromQuery(Name = "name")] string name, [FromQuery(Name = "description")] string description, [FromQuery(Name = "date")] DateTime date, [FromQuery(Name = "quota")] int quota, [FromQuery(Name = "address")] string address, [FromQuery(Name = "category")] string category, [FromQuery(Name = "city")] string city)        
        {
            return View(new ActivityDetailVM()
            {
                Id = id,
                Organizer = organizer,
                Name = name,
                Description = description,
                ActivityDate = date,
                Quota = quota,
                Address = address,
                Category = category,
                City = city
            });
        }
        [HttpPost("ConfirmActivity")]
        public async Task<IActionResult> ConfirmActivity(ActivityDetailVM activityDetailVM)
        {
            ViewData["ConfirmActivityMessage"] = null;
            ConfirmActivityVM confirmActivityVM = new() { Id = activityDetailVM.Id, IsConfirm = activityDetailVM.IsConfirm };
            var resultApi = await _apiService.ApiPutResponse(confirmActivityVM, "url", "Home", "confirm-activity");
            DataResult<string> responseCurrentUser = await HasCurrentUser();
            if (!responseCurrentUser.IsSuccess)
            {
                TempData["LogoutData"] = JsonConvert.SerializeObject(responseCurrentUser.Message);
                return RedirectToAction("Login", "Home", new { area = "" });
            }
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
