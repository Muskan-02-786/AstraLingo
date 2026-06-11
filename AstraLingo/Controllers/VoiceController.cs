using Microsoft.AspNetCore.Mvc;

namespace AstraLingo.Controllers
{
    public class VoiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}