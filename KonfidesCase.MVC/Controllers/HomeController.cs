using KonfidesCase.MVC.BusinessLogic.Services;
using KonfidesCase.MVC.Models;
using KonfidesCase.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace KonfidesCase.MVC.Controllers
{
    public class HomeController : Controller
    {
        #region Fields & Constructor
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IApiService _apiService;
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory, IApiService apiService)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _apiService = apiService;
        }
        #endregion

        #region Actions
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {            
            var resultApi = await _apiService.ApiPostResponse<LoginVM>(loginVM, "url", "Home", "login");
            _apiService.ApiDeserializeResult(resultApi, out DataResult<UserInfo> responseData);            
            if (!responseData.IsSuccess)
            {
                ViewData["LoginMessage"] = responseData.Message;
                return View();
            }            
            TempData["IndexData"] = JsonConvert.SerializeObject(responseData);
            return RedirectToAction("Index", "Home", new { area = responseData.Data!.RoleName });
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            var jsonUser = JsonConvert.SerializeObject(registerVM);
            HttpClient client = _httpClientFactory.CreateClient("url");
            var data = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Home/register", data);
            var result = await response.Content.ReadAsStringAsync();
            DataResult<UserInfo> responseRegister = JsonConvert.DeserializeObject<DataResult<UserInfo>>(result)!;            
            return RedirectToAction(nameof(Login), responseRegister.Data);            
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpClient client = _httpClientFactory.CreateClient("url");
            var response = await client.GetAsync("Home/Logout");
            var result = await response.Content.ReadAsStringAsync();
            DataResult<string> responseLogin = JsonConvert.DeserializeObject<DataResult<string>>(result)!;
            if (!responseLogin.IsSuccess)
            {
                return RedirectToAction("Index", "");
            }
            return RedirectToAction(nameof(Login));
        }

        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}