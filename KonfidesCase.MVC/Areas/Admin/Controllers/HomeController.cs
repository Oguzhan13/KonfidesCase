using KonfidesCase.MVC.Models;
using KonfidesCase.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Net.Http;
using System.Text;

namespace KonfidesCase.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index(UserInfo userInfo)
        {
            if (string.IsNullOrEmpty(userInfo.Email))
            {
                var tempData = TempData["IndexData"];
                if (tempData is null)
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                userInfo = JsonConvert.DeserializeObject<UserInfo>((string)tempData)!;                
            }
            return View(userInfo);
        }

        [HttpGet("change-password")]
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

            return RedirectToAction(nameof(Index), responseChangePassword.Data);
        }
    }
}
