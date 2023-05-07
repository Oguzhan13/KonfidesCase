using KonfidesCase.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace KonfidesCase.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class HomeController : Controller
    {
        public IActionResult Index(UserInfo userInfo)
        {            
            return View(userInfo);
        }
        
    }
}
