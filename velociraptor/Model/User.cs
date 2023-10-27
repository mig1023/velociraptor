using System.ComponentModel.DataAnnotations;

namespace velociraptor.Model
{
    public class User
    {
        [Key]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime RegisterDate { get; set; }
    }
}
