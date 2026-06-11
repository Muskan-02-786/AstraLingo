using System.ComponentModel.DataAnnotations;

namespace AstraLingo.Models
{
    public class Lesson
    {
        [Key]
        public int LessonId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int LanguageId { get; set; }

        public Language? Language { get; set; }
    }
}