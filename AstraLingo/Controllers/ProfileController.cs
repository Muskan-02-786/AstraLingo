using AstraLingo.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AstraLingo.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProfileController(
            ApplicationDbContext context,
            IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        public IActionResult Index()
        {
            if (
                HttpContext.Session.GetString(
                    "UserName") == null)
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }

            var user =
                _context.Users
                .FirstOrDefault();

            return View(user);
        }

        [HttpPost]
        public IActionResult Upload(
            IFormFile file)

        {
            var user =
                _context.Users
                .FirstOrDefault();


            if (file != null &&
                user != null)
            {
                string fileName =
                    file.FileName;


                string path =
                    Path.Combine(

                        _env.WebRootPath,
                        "images",
                        fileName

                    );


                using (var stream =
                    new FileStream(

                        path,
                        FileMode.Create

                    ))
                {
                    file.CopyTo(
                        stream);
                }


                user.ProfileImage =
                    fileName;


                _context.SaveChanges();
            }


            // go to dashboard after upload
            return RedirectToAction(

                "Index",
                "Dashboard"

            );
        }


        public IActionResult RemovePhoto()
        {
            var user =
                _context.Users
                .FirstOrDefault();


            if (user != null &&
                !string.IsNullOrEmpty(
                    user.ProfileImage))
            {
                string path =
                    Path.Combine(

                        _env.WebRootPath,
                        "images",
                        user.ProfileImage

                    );


                if (System.IO.File.Exists(
                    path))
                {
                    System.IO.File.Delete(
                        path);
                }


                user.ProfileImage =
                    null;


                _context.SaveChanges();
            }


            return RedirectToAction(
                "Index");
        }
    }
}