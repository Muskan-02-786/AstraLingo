using AstraLingo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace AstraLingo.Controllers
{
    public class QuizController : Controller
    {
        private readonly IConfiguration _configuration;

        public QuizController(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // =========================
        // DISPLAY QUIZZES
        // =========================

        public IActionResult Index(int id)
        {
            List<Quiz> quizzes =
                new List<Quiz>();


            string conStr =
                _configuration
                .GetConnectionString(
                    "DefaultConnection")!;


            SqlConnection conn =
                new SqlConnection(conStr);


            string query =
                @"SELECT * FROM Quizzes

                WHERE LanguageId=@id";


            SqlCommand cmd =
                new SqlCommand(
                    query,
                    conn);


            cmd.Parameters.AddWithValue(
                "@id",
                id);


            conn.Open();

            SqlDataReader dr =
                cmd.ExecuteReader();


            while (dr.Read())
            {
                Quiz quiz =
                    new Quiz();

                quiz.QuizId =
                    Convert.ToInt32(
                        dr["QuizId"]);

                quiz.Question =
                    dr["Question"].ToString()!;

                quiz.OptionA =
                    dr["OptionA"].ToString()!;

                quiz.OptionB =
                    dr["OptionB"].ToString()!;

                quiz.OptionC =
                    dr["OptionC"].ToString()!;

                quiz.OptionD =
                    dr["OptionD"].ToString()!;

                quiz.CorrectAnswer =
                    dr["CorrectAnswer"].ToString()!;

                quiz.LanguageId =
                    Convert.ToInt32(
                        dr["LanguageId"]);


                quizzes.Add(quiz);
            }

            conn.Close();

            return View(quizzes);
        }



        // =========================
        // MANAGE QUIZZES
        // =========================

        public IActionResult ManageQuiz()
        {
            List<Quiz> quizzes =
                new List<Quiz>();


            string conStr =
                _configuration
                .GetConnectionString(
                    "DefaultConnection")!;


            SqlConnection conn =
                new SqlConnection(conStr);


            string query =
                "SELECT * FROM Quizzes";


            SqlCommand cmd =
                new SqlCommand(
                    query,
                    conn);


            conn.Open();

            SqlDataReader dr =
                cmd.ExecuteReader();


            while (dr.Read())
            {
                Quiz quiz =
                    new Quiz();

                quiz.QuizId =
                    Convert.ToInt32(
                        dr["QuizId"]);

                quiz.Question =
                    dr["Question"].ToString()!;

                quiz.OptionA =
                    dr["OptionA"].ToString()!;

                quiz.OptionB =
                    dr["OptionB"].ToString()!;

                quiz.OptionC =
                    dr["OptionC"].ToString()!;

                quiz.OptionD =
                    dr["OptionD"].ToString()!;

                quiz.CorrectAnswer =
                    dr["CorrectAnswer"].ToString()!;

                quiz.LanguageId =
                    Convert.ToInt32(
                        dr["LanguageId"]);


                quizzes.Add(quiz);
            }

            conn.Close();

            return View(quizzes);
        }



        // =========================
        // CREATE QUIZ
        // =========================

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Quiz quiz)
        {
            string conStr =
                _configuration
                .GetConnectionString(
                    "DefaultConnection")!;


            SqlConnection conn =
                new SqlConnection(conStr);


            string query =
                @"INSERT INTO Quizzes
                (
                    Question,
                    OptionA,
                    OptionB,
                    OptionC,
                    OptionD,
                    CorrectAnswer,
                    LanguageId
                )

                VALUES

                (
                    @Question,
                    @OptionA,
                    @OptionB,
                    @OptionC,
                    @OptionD,
                    @CorrectAnswer,
                    @LanguageId
                )";


            SqlCommand cmd =
                new SqlCommand(
                    query,
                    conn);


            cmd.Parameters.AddWithValue(
                "@Question",
                quiz.Question);

            cmd.Parameters.AddWithValue(
                "@OptionA",
                quiz.OptionA);

            cmd.Parameters.AddWithValue(
                "@OptionB",
                quiz.OptionB);

            cmd.Parameters.AddWithValue(
                "@OptionC",
                quiz.OptionC);

            cmd.Parameters.AddWithValue(
                "@OptionD",
                quiz.OptionD);

            cmd.Parameters.AddWithValue(
                "@CorrectAnswer",
                quiz.CorrectAnswer);

            cmd.Parameters.AddWithValue(
                "@LanguageId",
                quiz.LanguageId);


            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();


            return RedirectToAction(
                "ManageQuiz");
        }



        // =========================
        // EDIT QUIZ
        // =========================

        public IActionResult Edit(int id)
        {
            Quiz quiz =
                new Quiz();


            string conStr =
                _configuration
                .GetConnectionString(
                    "DefaultConnection")!;


            SqlConnection conn =
                new SqlConnection(conStr);


            string query =
                "SELECT * FROM Quizzes WHERE QuizId=@id";


            SqlCommand cmd =
                new SqlCommand(
                    query,
                    conn);


            cmd.Parameters.AddWithValue(
                "@id",
                id);


            conn.Open();

            SqlDataReader dr =
                cmd.ExecuteReader();


            if (dr.Read())
            {
                quiz.QuizId =
                    Convert.ToInt32(
                        dr["QuizId"]);

                quiz.Question =
                    dr["Question"].ToString()!;

                quiz.OptionA =
                    dr["OptionA"].ToString()!;

                quiz.OptionB =
                    dr["OptionB"].ToString()!;

                quiz.OptionC =
                    dr["OptionC"].ToString()!;

                quiz.OptionD =
                    dr["OptionD"].ToString()!;

                quiz.CorrectAnswer =
                    dr["CorrectAnswer"].ToString()!;

                quiz.LanguageId =
                    Convert.ToInt32(
                        dr["LanguageId"]);
            }

            conn.Close();

            return View(quiz);
        }



        [HttpPost]
        public IActionResult Edit(Quiz quiz)
        {
            string conStr =
                _configuration
                .GetConnectionString(
                    "DefaultConnection")!;


            SqlConnection conn =
                new SqlConnection(conStr);


            string query =
                @"UPDATE Quizzes

                SET

                Question=@Question,
                OptionA=@OptionA,
                OptionB=@OptionB,
                OptionC=@OptionC,
                OptionD=@OptionD,
                CorrectAnswer=@CorrectAnswer,
                LanguageId=@LanguageId

                WHERE QuizId=@QuizId";


            SqlCommand cmd =
                new SqlCommand(
                    query,
                    conn);


            cmd.Parameters.AddWithValue(
                "@QuizId",
                quiz.QuizId);

            cmd.Parameters.AddWithValue(
                "@Question",
                quiz.Question);

            cmd.Parameters.AddWithValue(
                "@OptionA",
                quiz.OptionA);

            cmd.Parameters.AddWithValue(
                "@OptionB",
                quiz.OptionB);

            cmd.Parameters.AddWithValue(
                "@OptionC",
                quiz.OptionC);

            cmd.Parameters.AddWithValue(
                "@OptionD",
                quiz.OptionD);

            cmd.Parameters.AddWithValue(
                "@CorrectAnswer",
                quiz.CorrectAnswer);

            cmd.Parameters.AddWithValue(
                "@LanguageId",
                quiz.LanguageId);


            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();


            return RedirectToAction(
                "ManageQuiz");
        }



        // =========================
        // DELETE QUIZ
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
                "DELETE FROM Quizzes WHERE QuizId=@id";


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
                "ManageQuiz");
        }



        // =========================
        // COMPLETE QUIZ
        // =========================

        public IActionResult CompleteQuiz()
        {
            string? email =
                HttpContext.Session.GetString(
                    "UserEmail");


            if (email == null)
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }


            string conStr =
                _configuration
                .GetConnectionString(
                    "DefaultConnection")!;


            SqlConnection conn =
                new SqlConnection(conStr);


            conn.Open();


            string updateQuery =
                @"UPDATE Users

                SET XP = XP + 10

                WHERE Email=@Email";


            SqlCommand updateCmd =
                new SqlCommand(
                    updateQuery,
                    conn);


            updateCmd.Parameters.AddWithValue(
                "@Email",
                email);


            updateCmd.ExecuteNonQuery();


            string levelQuery =
                @"UPDATE Users

                SET Level = (XP / 50) + 1

                WHERE Email=@Email";


            SqlCommand levelCmd =
                new SqlCommand(
                    levelQuery,
                    conn);


            levelCmd.Parameters.AddWithValue(
                "@Email",
                email);


            levelCmd.ExecuteNonQuery();


            conn.Close();


            return RedirectToAction(

                "Index",

                "Dashboard"

            );
        }
    }
}