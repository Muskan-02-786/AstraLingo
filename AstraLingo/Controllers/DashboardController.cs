using AstraLingo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace AstraLingo.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IConfiguration _configuration;

        public DashboardController(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // =========================
        // USER DASHBOARD
        // =========================

        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]

        public IActionResult Index()
        {
            // =========================
            // LOGIN VALIDATION
            // =========================

            if (
                HttpContext.Session.GetString(
                    "UserName") == null)
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }


            User user =
                new User();


            string? email =
                HttpContext.Session.GetString(
                    "UserEmail");


            string conStr =
                _configuration
                .GetConnectionString(
                    "DefaultConnection")!;


            SqlConnection conn =
                new SqlConnection(conStr);


            string query =
                "SELECT * FROM Users WHERE Email=@Email";


            SqlCommand cmd =
                new SqlCommand(query, conn);


            cmd.Parameters.AddWithValue(
                "@Email",
                email);


            conn.Open();

            SqlDataReader dr =
                cmd.ExecuteReader();


            if (dr.Read())
            {
                user.UserId =
                    Convert.ToInt32(
                        dr["UserId"]);

                user.Name =
                    dr["Name"].ToString()!;

                user.Email =
                    dr["Email"].ToString()!;

                user.XP =
                    Convert.ToInt32(
                        dr["XP"]);

                user.Level =
                    Convert.ToInt32(
                        dr["Level"]);

                user.Streak =
                    Convert.ToInt32(
                        dr["Streak"]);

                user.ProfileImage =
                    dr["ProfileImage"]
                    .ToString()!;
            }

            conn.Close();


            // =========================
            // LEVEL BADGES
            // =========================

            if (user.Level >= 10)
            {
                ViewBag.Badge =
                    "👑 Master";
            }
            else if (user.Level >= 5)
            {
                ViewBag.Badge =
                    "🔥 Pro Learner";
            }
            else
            {
                ViewBag.Badge =
                    "🌱 Beginner";
            }


            return View(user);
        }
    }
}