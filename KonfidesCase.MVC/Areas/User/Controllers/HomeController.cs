using KonfidesCase.MVC.Areas.User.ViewModels;

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
        private readonly IHttpContextAccessor _contextAccessor;
        public HomeController(IHttpClientFactory httpClientFactory, IApiService apiService, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _httpClientFactory = httpClientFactory;
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
            if (string.IsNullOrEmpty(resultApi))
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }            
            DataResult<string> responseCurrentUser = await HasCurrentUser();
            if (!responseCurrentUser.IsSuccess)
            {
                TempData["LogoutData"] = JsonConvert.SerializeObject(responseCurrentUser.Message);
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            DataResult<UserInfoVM> responseData = JsonConvert.DeserializeObject<DataResult<UserInfoVM>>(resultApi)!;
            responseData.Data!.Password = changePasswordVM.NewPassword;
            string userInfo = JsonConvert.SerializeObject(responseData);
            _contextAccessor.HttpContext!.Session.SetString(responseData.Data.Email, userInfo);
            return RedirectToAction("Index", "Home", new { area = "user" });
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

        #region Activity Actions
        [HttpGet("CreateActivity")]
        public async Task<IActionResult> CreateActivity()
        {            
            var resultCategories = await _apiService.ApiGetResponse("url", "Home", "get-categories");
            var resultCities = await _apiService.ApiGetResponse("url", "Home", "get-cities");
            DataResult<string> responseCurrentUser = await HasCurrentUser();
            if (!responseCurrentUser.IsSuccess)
            {
                TempData["LogoutData"] = JsonConvert.SerializeObject(responseCurrentUser.Message);
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            DataResult<ICollection<CategoryVM>> responseCategories = JsonConvert.DeserializeObject<DataResult<ICollection<CategoryVM>>>(resultCategories)!;            
            DataResult<ICollection<CityVM>> responseCities = JsonConvert.DeserializeObject<DataResult<ICollection<CityVM>>>(resultCities)!;
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
            DataResult<string> responseCurrentUser = await HasCurrentUser();
            if (!responseCurrentUser.IsSuccess)
            {
                TempData["LogoutData"] = JsonConvert.SerializeObject(responseCurrentUser.Message);
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            DataResult<ActivityVM> responseData = JsonConvert.DeserializeObject<DataResult<ActivityVM>>(resultApi)!;
            if (!responseData.IsSuccess)
            {
                ViewData["CreateActivityMessage"] = responseData.Message;
                return View();
            }
            return RedirectToAction("GetActivities", "Home", new { area = "user" });
        }

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

        [HttpGet("UpdateActivity")]
        public IActionResult UpdateActivity([FromQuery(Name = "id")] Guid activityId)
        {
            return View(new UpdateActivityVM() { Id =  activityId });
        }
        [HttpPost("UpdateActivity")]
        public async Task<IActionResult> UpdateActivity(UpdateActivityVM updateActivityVM)
        {
            var resultApi = await _apiService.ApiPutResponse(updateActivityVM, "url", "Home", "update-activity");
            DataResult<string> responseCurrentUser = await HasCurrentUser();
            if (!responseCurrentUser.IsSuccess)
            {
                TempData["LogoutData"] = JsonConvert.SerializeObject(responseCurrentUser.Message);
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            DataResult<ActivityVM> responseData = JsonConvert.DeserializeObject<DataResult<ActivityVM>>(resultApi)!;
            if (!responseData.IsSuccess)
            {                
                return View();
            }
            return RedirectToAction("GetMyCreatedActivities", "Home", new { area = "user" });
        }

        [HttpGet("CancelActivity")]        
        public async Task<IActionResult> CancelActivity([FromQuery(Name = "id")] Guid activityId)
        {
            //return View(new CancelActivityVM() { Id = activityId });
            var resultApi = await _apiService.ApiPostResponse(new CancelActivityVM() { Id = activityId }, "url", "Home", "cancel-activity");
            DataResult<string> responseCurrentUser = await HasCurrentUser();
            if (!responseCurrentUser.IsSuccess)
            {
                TempData["LogoutData"] = JsonConvert.SerializeObject(responseCurrentUser.Message);
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            DataResult<ActivityVM> responseData = JsonConvert.DeserializeObject<DataResult<ActivityVM>>(resultApi)!;
            return RedirectToAction("GetActivities", "Home", new { area = "user" });
        }        

        [HttpGet("GetMyCreatedActivities")]
        public async Task<IActionResult> GetMyCreatedActivities()
        {
            var resultCategories = await _apiService.ApiGetResponse("url", "Home", "get-categories");
            var resultCities = await _apiService.ApiGetResponse("url", "Home", "get-cities");
            var resultApi = await _apiService.ApiGetResponse("url", "Home", "created-activities");
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

        [HttpGet("GetAttendedActivities")]
        public async Task<IActionResult> GetAttendedActivities()
        {
            var resultCategories = await _apiService.ApiGetResponse("url", "Home", "get-categories");
            var resultCities = await _apiService.ApiGetResponse("url", "Home", "get-cities");
            var resultApi = await _apiService.ApiGetResponse("url", "Home", "attended-activities");
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
        #endregion

        #region Ticket Action
        [HttpGet("BuyTicket")]
        public async Task<IActionResult> BuyTicket([FromQuery(Name ="id")] Guid activityId)
        {
            DataResult<string> responseCurrentUser = await HasCurrentUser();
            if (!responseCurrentUser.IsSuccess)
            {
                TempData["LogoutData"] = JsonConvert.SerializeObject(responseCurrentUser.Message);
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            var sessionData = _contextAccessor.HttpContext!.Session.GetString(responseCurrentUser.Data!);
            DataResult<UserInfoVM> userInfoSession = JsonConvert.DeserializeObject<DataResult<UserInfoVM>>(sessionData!)!;
            return View(new BuyTicketVM() { ActivityId = activityId, UserId = userInfoSession.Data!.Id });
        }
        [HttpPost("BuyTicket")]
        public async Task<IActionResult> BuyTicket(BuyTicketVM buyTicketVM)
        {
            var resultApi = await _apiService.ApiPostResponse(buyTicketVM, "url", "Home", "buy-ticket");
            DataResult<string> responseCurrentUser = await HasCurrentUser();
            if (!responseCurrentUser.IsSuccess)
            {
                TempData["LogoutData"] = JsonConvert.SerializeObject(responseCurrentUser.Message);
                return RedirectToAction("Login", "Home", new { area = "" });
            }
            DataResult<TicketVM> responseData = JsonConvert.DeserializeObject<DataResult<TicketVM>>(resultApi)!;
            if (!responseData.IsSuccess)
            {                
                return View();
            }
            return RedirectToAction("GetActivities", "Home", new { area = "user" });
        }
        #endregion
    }
}
