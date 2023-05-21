using KonfidesCase.MVC.BusinessLogic.Services;
using KonfidesCase.MVC.Models;
using KonfidesCase.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace KonfidesCase.MVC.Controllers
{
    public class HomeController : Controller
    {
        #region Fields & Constructor
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IApiService _apiService;
        private readonly IHttpContextAccessor _contextAccessor;
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory, IApiService apiService, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _apiService = apiService;
            _contextAccessor = contextAccessor;
        }
        #endregion

        #region Login Action
        [HttpGet]
        public IActionResult Login()
        {
            var tempData = TempData["LogoutData"];
            if (tempData is null)
            {
                return View();
            }            
            ViewData["LogoutMessage"] = tempData;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            ViewData["LogoutMessage"] = null;
            var resultApi = await _apiService.ApiPostResponse(loginVM, "url", "Home", "login");            
            DataResult<UserInfoVM> responseData =  JsonConvert.DeserializeObject<DataResult<UserInfoVM>>(resultApi)!;            
            if (!responseData.IsSuccess)
            {
                ViewData["LoginMessage"] = responseData.Message;
                return View();
            }
            TempData["IndexData"] = JsonConvert.SerializeObject(responseData);
            string currentUserEmail = responseData.Data!.Email;
            string userInfo = JsonConvert.SerializeObject(responseData);
            _contextAccessor.HttpContext!.Session.SetString(currentUserEmail, userInfo);
            return RedirectToAction("Index", "Home", new { area = responseData.Data!.RoleName });
        }
        #endregion

        #region Register Action
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            ViewData["RegisterMessage"] = null;
            var resultApi = await _apiService.ApiPostResponse(registerVM, "url", "Home", "register");            
            DataResult<UserInfoVM> responseData = JsonConvert.DeserializeObject<DataResult<UserInfoVM>>(resultApi)!;
            if (!responseData.IsSuccess)
            {
                ViewData["RegisterMessage"] = responseData.Message;
                return View();
            }
            return RedirectToAction(nameof(Login), responseData.Data);            
        }                
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}