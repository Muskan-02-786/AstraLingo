using AstraLingo.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AstraLingo.Controllers
{
    public class CertificateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CertificateController(
            ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var user =
                _context.Users
                .FirstOrDefault();

            return View(user);
        }
    }
}