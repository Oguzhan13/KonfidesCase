using Microsoft.AspNetCore.Mvc;

namespace KonfidesCase.MVC.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
