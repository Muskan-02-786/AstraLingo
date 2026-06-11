using AstraLingo.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AstraLingo.Controllers
{
    public class LeaderboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaderboardController(
            ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var users =
                _context.Users
                .OrderByDescending(
                    x => x.XP)
                .ToList();

            return View(users);
        }
    }
}