using AstraLingo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace AstraLingo.Controllers
{
    public class LanguageController : Controller
    {
        private readonly IConfiguration _configuration;

        public LanguageController(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // =========================
        // USER LANGUAGE PAGE
        // =========================

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


            List<Language> languages =
                new List<Language>();


            int userLevel = 1;


            string? email =
                HttpContext.Session.GetString(
                    "UserEmail");


            string conStr =
                _configuration
                .GetConnectionString(
                    "DefaultConnection")!;


            SqlConnection conn =
                new SqlConnection(conStr);


            conn.Open();


            // GET USER LEVEL

            string userQuery =
                "SELECT * FROM Users WHERE Email=@Email";


            SqlCommand userCmd =
                new SqlCommand(userQuery, conn);


            userCmd.Parameters.AddWithValue(
                "@Email",
                email);


            SqlDataReader userDr =
                userCmd.ExecuteReader();


            if (userDr.Read())
            {
                userLevel =
                    Convert.ToInt32(
                        userDr["Level"]);
            }

            userDr.Close();


            ViewBag.UserLevel =
                userLevel;


            // GET LANGUAGES

            string query =
                "SELECT * FROM Languages";


            SqlCommand cmd =
                new SqlCommand(query, conn);


            SqlDataReader dr =
                cmd.ExecuteReader();


            while (dr.Read())
            {
                Language lang =
                    new Language();

                lang.LanguageId =
                    Convert.ToInt32(
                        dr["LanguageId"]);

                lang.Name =
                    dr["Name"].ToString()!;

                lang.Description =
                    dr["Description"].ToString()!;

                lang.RequiredLevel =
                    Convert.ToInt32(
                        dr["RequiredLevel"]);


                languages.Add(lang);
            }

            conn.Close();

            return View(languages);
        }



        // =========================
        // ADMIN MANAGE LANGUAGE
        // =========================

        public IActionResult ManageLanguage()
        {
            List<Language> languages =
                new List<Language>();


            string conStr =
                _configuration
                .GetConnectionString(
                    "DefaultConnection")!;


            SqlConnection conn =
                new SqlConnection(conStr);


            string query =
                "SELECT * FROM Languages";


            SqlCommand cmd =
                new SqlCommand(query, conn);


            conn.Open();

            SqlDataReader dr =
                cmd.ExecuteReader();


            while (dr.Read())
            {
                Language lang =
                    new Language();

                lang.LanguageId =
                    Convert.ToInt32(
                        dr["LanguageId"]);

                lang.Name =
                    dr["Name"].ToString()!;

                lang.Description =
                    dr["Description"].ToString()!;

                lang.RequiredLevel =
                    Convert.ToInt32(
                        dr["RequiredLevel"]);


                languages.Add(lang);
            }

            conn.Close();

            return View(languages);
        }



        // =========================
        // CREATE LANGUAGE
        // =========================

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Language language)
        {
            string conStr =
                _configuration
                .GetConnectionString(
                    "DefaultConnection")!;


            SqlConnection conn =
                new SqlConnection(conStr);


            string query =
                @"INSERT INTO Languages
                (
                    Name,
                    Description,
                    RequiredLevel
                )

                VALUES
                (
                    @Name,
                    @Description,
                    @RequiredLevel
                )";


            SqlCommand cmd =
                new SqlCommand(query, conn);


            cmd.Parameters.AddWithValue(
                "@Name",
                language.Name);

            cmd.Parameters.AddWithValue(
                "@Description",
                language.Description);

            cmd.Parameters.AddWithValue(
                "@RequiredLevel",
                language.RequiredLevel);


            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();


            return RedirectToAction(
                "ManageLanguage");
        }



        // =========================
        // EDIT LANGUAGE
        // =========================

        public IActionResult Edit(int id)
        {
            Language language =
                new Language();


            string conStr =
                _configuration
                .GetConnectionString(
                    "DefaultConnection")!;


            SqlConnection conn =
                new SqlConnection(conStr);


            string query =
                "SELECT * FROM Languages WHERE LanguageId=@id";


            SqlCommand cmd =
                new SqlCommand(query, conn);


            cmd.Parameters.AddWithValue(
                "@id",
                id);


            conn.Open();

            SqlDataReader dr =
                cmd.ExecuteReader();


            if (dr.Read())
            {
                language.LanguageId =
                    Convert.ToInt32(
                        dr["LanguageId"]);

                language.Name =
                    dr["Name"].ToString()!;

                language.Description =
                    dr["Description"].ToString()!;

                language.RequiredLevel =
                    Convert.ToInt32(
                        dr["RequiredLevel"]);
            }

            conn.Close();

            return View(language);
        }



        [HttpPost]
        public IActionResult Edit(Language language)
        {
            string conStr =
                _configuration
                .GetConnectionString(
                    "DefaultConnection")!;


            SqlConnection conn =
                new SqlConnection(conStr);


            string query =
                @"UPDATE Languages

                SET

                Name=@Name,
                Description=@Description,
                RequiredLevel=@RequiredLevel

                WHERE LanguageId=@LanguageId";


            SqlCommand cmd =
                new SqlCommand(query, conn);


            cmd.Parameters.AddWithValue(
                "@LanguageId",
                language.LanguageId);

            cmd.Parameters.AddWithValue(
                "@Name",
                language.Name);

            cmd.Parameters.AddWithValue(
                "@Description",
                language.Description);

            cmd.Parameters.AddWithValue(
                "@RequiredLevel",
                language.RequiredLevel);


            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();


            return RedirectToAction(
                "ManageLanguage");
        }



        // =========================
        // DELETE LANGUAGE
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
                "DELETE FROM Languages WHERE LanguageId=@id";


            SqlCommand cmd =
                new SqlCommand(query, conn);


            cmd.Parameters.AddWithValue(
                "@id",
                id);


            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();


            return RedirectToAction(
                "ManageLanguage");
        }
    }
}