using System.ComponentModel.DataAnnotations;

namespace AstraLingo.Models
{
    public class User
    {
        public int UserId { get; set; }


        [Required(
            ErrorMessage =
            "Please enter name")]
        public string Name { get; set; } = "";


        [Required(
            ErrorMessage =
            "Please enter email")]
        public string Email { get; set; } = "";


        [Required(
            ErrorMessage =
            "Please enter password")]
        public string Password { get; set; } = "";


        public int XP { get; set; }

        public int Level { get; set; } = 1;

        public int Streak { get; set; }

        public string? ProfileImage { get; set; }
    }
}