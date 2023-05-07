using KonfidesCase.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace KonfidesCase.MVC.Areas.User.Controllers
{
    [Area("User")]
    [Route("user/[controller]")]
    public class HomeController : Controller
    {
        public IActionResult Index(UserInfo userInfo)
        {
            return View(userInfo);
        }
    }
}
