using Microsoft.AspNetCore.Mvc;

namespace AstraLingo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}