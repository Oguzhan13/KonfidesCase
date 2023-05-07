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
            bool isAdmin = responseLogin.Data!.RoleName.Equals("admin");
            return isAdmin ? View("~/Areas/Admin/Views/Home/Index.cshtml", responseLogin.Data) : View("~/Areas/User/Views/Home/Index.cshtml", responseLogin.Data);
            //return isAdmin ? RedirectToAction(nameof(Index), responseLogin.Data) : RedirectToAction(nameof(Privacy), responseLogin.Data);
            //return RedirectToAction("Index", "Home", new { area = "admin" });
        }


        // Silinecek -----------------------------------------------------------------------------
        [HttpGet]
        public IActionResult Index(UserInfo userInfo)
        {
            return View(userInfo);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}