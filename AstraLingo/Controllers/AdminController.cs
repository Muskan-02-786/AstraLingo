using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace AstraLingo.Controllers
{
    public class AdminController : Controller
    {
        private readonly IConfiguration _configuration;

        public AdminController(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // =========================
        // ADMIN DASHBOARD
        // =========================

        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]

        public IActionResult Dashboard()
        {
            // =========================
            // ADMIN VALIDATION
            // =========================

            string? role =
                HttpContext.Session.GetString(
                    "Role");


            if (role != "Admin")
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }


            int totalUsers = 0;
            int totalLanguages = 0;
            int totalQuizzes = 0;
            int totalLessons = 0;


            string conStr =
                _configuration
                .GetConnectionString(
                    "DefaultConnection")!;


            SqlConnection conn =
                new SqlConnection(conStr);


            conn.Open();


            // =========================
            // TOTAL USERS
            // =========================

            SqlCommand userCmd =
                new SqlCommand(
                    "SELECT COUNT(*) FROM Users",
                    conn);

            totalUsers =
                (int)userCmd.ExecuteScalar()!;


            // =========================
            // TOTAL LANGUAGES
            // =========================

            SqlCommand langCmd =
                new SqlCommand(
                    "SELECT COUNT(*) FROM Languages",
                    conn);

            totalLanguages =
                (int)langCmd.ExecuteScalar()!;


            // =========================
            // TOTAL QUIZZES
            // =========================

            SqlCommand quizCmd =
                new SqlCommand(
                    "SELECT COUNT(*) FROM Quizzes",
                    conn);

            totalQuizzes =
                (int)quizCmd.ExecuteScalar()!;


            // =========================
            // TOTAL LESSONS
            // =========================

            SqlCommand lessonCmd =
                new SqlCommand(
                    "SELECT COUNT(*) FROM Lessons",
                    conn);

            totalLessons =
                (int)lessonCmd.ExecuteScalar()!;


            conn.Close();


            // =========================
            // SEND DATA TO VIEW
            // =========================

            ViewBag.TotalUsers =
                totalUsers;

            ViewBag.TotalLanguages =
                totalLanguages;

            ViewBag.TotalQuizzes =
                totalQuizzes;

            ViewBag.TotalLessons =
                totalLessons;


            return View();
        }
    }
}