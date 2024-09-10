using Microsoft.AspNetCore.Mvc;

namespace DernSupport2.Controllers.MVC_Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
