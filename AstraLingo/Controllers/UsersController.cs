using AstraLingo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AstraLingo.Controllers
{
    public class UsersController : Controller
    {
        private readonly IConfiguration _configuration;

        public UsersController(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // =========================
        // SHOW USERS
        // =========================

        public IActionResult Index()
        {
            List<User> users =
                new List<User>();


            string conStr =
                _configuration
                .GetConnectionString(
                    "DefaultConnection");


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
                users.Add(

                    new User
                    {
                        UserId =
                            Convert.ToInt32(
                                dr["UserId"]),

                        Name =
                              dr["Name"].ToString()!,

                        Email =
                              dr["Email"].ToString()!,

                        Password =
                               dr["Password"].ToString()!,

                        XP =
                            Convert.ToInt32(
                                dr["XP"]),

                        Level =
                            Convert.ToInt32(
                                dr["Level"]),

                        Streak =
                            Convert.ToInt32(
                                dr["Streak"])
                    }
                );
            }

            conn.Close();

            return View(users);
        }



      
        // CREATE
     

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(User user)
        {
            string? conStr =
     _configuration
     .GetConnectionString(
         "DefaultConnection");


            SqlConnection conn =
                new SqlConnection(conStr);


            string query =
                @"INSERT INTO Users
                (Name, Email, Password, XP, Level, Streak)

                VALUES

                (@Name, @Email, @Password, 0, 1, 0)";


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
                    "DefaultConnection");


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