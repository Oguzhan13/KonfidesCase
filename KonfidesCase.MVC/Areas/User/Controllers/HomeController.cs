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
        public HomeController(IHttpClientFactory httpClientFactory, IApiService apiService)
        {
            _httpClientFactory = httpClientFactory;
            _apiService = apiService;
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

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            var resultApi = await _apiService.ApiGetResponse("url", "Home", "logout");
            _apiService.ApiDeserializeResult(resultApi, out DataResult<string> responseData);
            TempData["LogoutData"] = JsonConvert.SerializeObject(responseData);            
            return RedirectToAction("Login", "Home");
        }
    }
}
