using System.ComponentModel.DataAnnotations;

namespace AstraLingo.Models
{
    public class Mission
    {
        [Key]
        public int MissionId { get; set; }

        public string Title { get; set; } = "";

        public string Description { get; set; } = "";

        public int LanguageId { get; set; }

        public string? SubmissionFile { get; set; }
    }
}