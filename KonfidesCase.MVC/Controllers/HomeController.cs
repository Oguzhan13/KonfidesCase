using KonfidesCase.MVC.Models;
using KonfidesCase.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace KonfidesCase.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            var jsonUser = JsonConvert.SerializeObject(loginVM);
            HttpClient client = _httpClientFactory.CreateClient("url");
            var data = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Home/login", data);
            var result = await response.Content.ReadAsStringAsync();
            DataResult<UserInfo> responseLogin = JsonConvert.DeserializeObject<DataResult<UserInfo>>(result)!;
            if (!responseLogin.IsSuccess)
            {
                return View();
            }
            //return View($"~/Areas/{responseLogin.Data!.RoleName}/Views/Home/Index.cshtml", responseLogin.Data);

            //return isAdmin ? RedirectToAction(nameof(Index), responseLogin.Data) : RedirectToAction(nameof(Privacy), responseLogin.Data);
            TempData["loginModel"] = JsonConvert.SerializeObject(responseLogin.Data);
            return RedirectToAction("Index", "Home", new { area = "admin" });
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
            var result = await client.GetAsync("Home/Logout");
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var jsonPassword = JsonConvert.SerializeObject(changePasswordVM);
            HttpClient client = _httpClientFactory.CreateClient("url");
            var data = new StringContent(jsonPassword, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("Home/change-password", data);
            var result = await response.Content.ReadAsStringAsync();
            DataResult<UserInfo> responseChangePassword = JsonConvert.DeserializeObject<DataResult<UserInfo>>(result)!;

            return View($"~/Areas/{responseChangePassword.Data!.RoleName}/Views/Home/Index.cshtml", responseChangePassword.Data);
            //return RedirectToAction(nameof(Index), responseChangePassword.Data);
        }

                        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}