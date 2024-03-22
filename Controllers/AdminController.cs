using Microsoft.AspNetCore.Mvc;

namespace blog.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UserId")==null)
            {
                return RedirectToAction("Login","Home");
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login","Home");
        }
    }
}
