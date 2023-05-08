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
        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        #endregion

        [HttpGet("index")]
        public IActionResult Index(DataResult<UserInfo> userInfo)
        {
            if (userInfo.Data is null)
            {
                var tempData = TempData["IndexData"];
                if (tempData is null)
                {
                    return RedirectToAction("Login", "Home", new { area = "" });
                }
                userInfo = JsonConvert.DeserializeObject<DataResult<UserInfo>>((string)tempData)!;
            }
            return View(userInfo);
        }
    }
}
