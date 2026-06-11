using AstraLingo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace AstraLingo.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;

        public AccountController(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // =========================
        // REGISTER PAGE
        // =========================

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(User user)
        {
            string conStr =
                _configuration
                .GetConnectionString(
                    "DefaultConnection")!;


            SqlConnection conn =
                new SqlConnection(conStr);


            string query =
                @"INSERT INTO Users
                (
                    Name,
                    Email,
                    Password,
                    XP,
                    Level,
                    Streak,
                    Role
                )

                VALUES

                (
                    @Name,
                    @Email,
                    @Password,
                    0,
                    1,
                    0,
                    'User'
                )";


            SqlCommand cmd =
                new SqlCommand(
                    query,
                    conn);


            cmd.Parameters.AddWithValue(
                "@Name",
                user.Name);

            cmd.Parameters.AddWithValue(
                "@Email",
                user.Email);

            cmd.Parameters.AddWithValue(
                "@Password",
                user.Password);


            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();


            return RedirectToAction(
                "Login");
        }



        // =========================
        // LOGIN PAGE
        // =========================

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(
            string email,
            string password)
        {
            string conStr =
                _configuration
                .GetConnectionString(
                    "DefaultConnection")!;


            SqlConnection conn =
                new SqlConnection(conStr);


            string query =
                @"SELECT * FROM Users

                WHERE Email=@Email
                AND Password=@Password";


            SqlCommand cmd =
                new SqlCommand(
                    query,
                    conn);


            cmd.Parameters.AddWithValue(
                "@Email",
                email);

            cmd.Parameters.AddWithValue(
                "@Password",
                password);


            conn.Open();

            SqlDataReader dr =
                cmd.ExecuteReader();


            if (dr.Read())
            {
                // CLEAR OLD SESSION
                HttpContext.Session.Clear();


                // STORE USERNAME
                HttpContext.Session.SetString(

                    "UserName",

                    dr["Name"].ToString()!

                );


                // STORE EMAIL
                HttpContext.Session.SetString(

                    "UserEmail",

                    dr["Email"].ToString()!

                );


                // STORE ROLE
                HttpContext.Session.SetString(

                    "Role",

                    dr["Role"].ToString()!

                );


                // GET ROLE
                string role =
                    dr["Role"].ToString()!;


                conn.Close();


                // =========================
                // ADMIN LOGIN
                // =========================

                if (role == "Admin")
                {
                    return RedirectToAction(

                        "Dashboard",

                        "Admin"

                    );
                }


                // =========================
                // USER LOGIN
                // =========================

                return RedirectToAction(

                    "Index",

                    "Dashboard"

                );
            }


            conn.Close();

            ViewBag.Error =
                "Invalid Login";


            return View();
        }



        // =========================
        // LOGOUT
        // =========================

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            HttpContext.Session.Remove(
                "UserName");

            HttpContext.Session.Remove(
                "UserEmail");

            HttpContext.Session.Remove(
                "Role");


            return RedirectToAction(

                "Login",

                "Account"

            );
        }
    }
}