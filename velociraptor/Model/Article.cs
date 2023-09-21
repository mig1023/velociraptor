using System.ComponentModel.DataAnnotations;

namespace velociraptor.Model
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastChange { get; set; }
    }
}
