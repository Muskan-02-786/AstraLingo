using AstraLingo.Data;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AstraLingo.Controllers
{
    public class RewardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RewardController(
            ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Spin()
        {
            var user =
                _context.Users.FirstOrDefault();

            if (user != null)
            {
                Random random =
                    new Random();

                int reward =
                    random.Next(1, 5);




                if (reward == 1)
                {
                    user.XP += 10;

                    TempData["Reward"] =
                        "🎉 You won +10 XP!";
                }


                else if (reward == 2)
                {
                    user.XP += 20;

                    TempData["Reward"] =
                        "🔥 You won +20 XP!";
                }


                else if (reward == 3)
                {
                    user.Streak += 1;

                    TempData["Reward"] =
                        "⭐ Bonus Streak +1!";
                }


                else
                {
                    user.XP += 50;

                    TempData["Reward"] =
                        "🏆 JACKPOT +50 XP!";
                }


                user.Level =
                    (user.XP / 50) + 1;

                _context.SaveChanges();
            }

            return View();
        }
    }
}