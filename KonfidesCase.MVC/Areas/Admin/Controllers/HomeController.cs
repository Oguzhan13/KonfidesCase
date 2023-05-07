using KonfidesCase.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace KonfidesCase.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {            
            var tempData = TempData["loginModel"];            
            if (tempData != null)
            {
                UserInfo userInfo = JsonConvert.DeserializeObject<UserInfo>((string)tempData);
                return View(userInfo);
            }
            return View();
        }
        
    }
}
