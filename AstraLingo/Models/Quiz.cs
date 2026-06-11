using System.ComponentModel.DataAnnotations;

namespace AstraLingo.Models
{
    public class Quiz
    {
        [Key]
        public int QuizId { get; set; }

        public string Question { get; set; }

        public string OptionA { get; set; }

        public string OptionB { get; set; }

        public string OptionC { get; set; }

        public string OptionD { get; set; }

        public string CorrectAnswer { get; set; }

        public int LanguageId { get; set; }
    }
}