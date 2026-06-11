using AstraLingo.Data;
using AstraLingo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AstraLingo.Controllers
{
    public class MissionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public MissionController(
            ApplicationDbContext context,
            IWebHostEnvironment env)
        {
            _context = context;
            _env = env;


            if (!_context.Missions.Any())
            {
                _context.Missions.AddRange(

                    new Mission
                    {
                        Title =
                            "English Mission",

                        Description =
                            "Order coffee in English.",

                        LanguageId = 1
                    },


                    new Mission
                    {
                        Title =
                            "Japanese Mission",

                        Description =
                            "Introduce yourself in Japanese.",

                        LanguageId = 2
                    },


                    new Mission
                    {
                        Title =
                            "Programming Mission",

                        Description =
                            "Build login page.",

                        LanguageId = 3
                    }

                );

                _context.SaveChanges();
            }
        }


        public IActionResult Index()
        {
            var missions =
                _context.Missions.ToList();

            return View(missions);
        }


        [HttpPost]
        public IActionResult Upload(
            int id,
            IFormFile file)
        {
            if (file != null)
            {
                string fileName =
                    file.FileName;

                string path =
                    Path.Combine(
                        _env.WebRootPath,
                        "missions",
                        fileName);

                using (var stream =
                    new FileStream(
                        path,
                        FileMode.Create))
                {
                    file.CopyTo(stream);
                }


                var mission =
                    _context.Missions
                    .FirstOrDefault(
                        x => x.MissionId == id);

                if (mission != null)
                {
                    mission.SubmissionFile =
                        fileName;

                    _context.SaveChanges();
                }
            }


            return RedirectToAction(
                "Index");
        }
    }
}