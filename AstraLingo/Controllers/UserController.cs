using AstraLingo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace AstraLingo.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;

        public UserController(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // =========================
        // DISPLAY USERS
        // =========================

        public IActionResult Index()
        {
            List<User> users =
                new List<User>();


            string conStr =
                _configuration
                .GetConnectionString(
                    "DefaultConnection")!;


            SqlConnection conn =
                new SqlConnection(conStr);


            string query =
                "SELECT * FROM Users";


            SqlCommand cmd =
                new SqlCommand(
                    query,
                    conn);


            conn.Open();

            SqlDataReader dr =
                cmd.ExecuteReader();


            while (dr.Read())
            {
                User user =
                    new User();

                user.UserId =
                    Convert.ToInt32(
                        dr["UserId"]);

                user.Name =
                    dr["Name"].ToString()!;

                user.Email =
                    dr["Email"].ToString()!;

                user.Password =
                    dr["Password"].ToString()!;

                user.XP =
                    Convert.ToInt32(
                        dr["XP"]);

                user.Level =
                    Convert.ToInt32(
                        dr["Level"]);

                user.Streak =
                    Convert.ToInt32(
                        dr["Streak"]);


                users.Add(user);
            }

            conn.Close();

            return View(users);
        }



        // =========================
        // CREATE PAGE
        // =========================

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(User user)
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
                    @XP,
                    @Level,
                    @Streak,
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

            cmd.Parameters.AddWithValue(
                "@XP",
                user.XP);

            cmd.Parameters.AddWithValue(
                "@Level",
                user.Level);

            cmd.Parameters.AddWithValue(
                "@Streak",
                user.Streak);


            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();


            return RedirectToAction(
                "Index");
        }



        // =========================
        // DELETE
        // =========================

        public IActionResult Delete(int id)
        {
            string conStr =
                _configuration
                .GetConnectionString(
                    "DefaultConnection")!;


            SqlConnection conn =
                new SqlConnection(conStr);


            string query =
                "DELETE FROM Users WHERE UserId=@id";


            SqlCommand cmd =
                new SqlCommand(
                    query,
                    conn);


            cmd.Parameters.AddWithValue(
                "@id",
                id);


            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();


            return RedirectToAction(
                "Index");
        }
    }
}