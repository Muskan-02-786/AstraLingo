using System.ComponentModel.DataAnnotations;

namespace AstraLingo.Models
{
    public class Language
    {
        [Key]
        public int LanguageId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int RequiredLevel { get; set; }

        public ICollection<Lesson>? Lessons { get; set; }
    }
}