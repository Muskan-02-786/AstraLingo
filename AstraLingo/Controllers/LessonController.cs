using AstraLingo.Data;
using AstraLingo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AstraLingo.Controllers
{
    public class LessonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LessonController(
            ApplicationDbContext context)
        {
            _context = context;


            // only add once
            if (!_context.Lessons.Any())
            {
                _context.Lessons.AddRange(

                    new Lesson
                    {
                        Title =
                            "English Basics",

                        Content =
                            "Hello, Good Morning, Thank You",

                        LanguageId = 1
                    },


                    new Lesson
                    {
                        Title =
                            "Japanese Basics",

                        Content =
                            "Konnichiwa, Arigato",

                        LanguageId = 2
                    },


                    new Lesson
                    {
                        Title =
                            "ASP.NET Page Life Cycle",

                        Content =
                            "Init → Load → PostBack → Render",

                        LanguageId = 3
                    },


                    new Lesson
                    {
                        Title =
                            "LINQ Basics",

                        Content =
                            "Where(), Select(), OrderBy()",

                        LanguageId = 4
                    }

                );

                _context.SaveChanges();
            }
        }


        public IActionResult Index(int id)
        {
            var lessons =
                _context.Lessons
                .Where(x =>
                    x.LanguageId == id)
                .ToList();


            ViewBag.LanguageId = id;

            return View(lessons);
        }
    }
}