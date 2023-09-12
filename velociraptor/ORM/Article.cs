using System.ComponentModel.DataAnnotations;

namespace velociraptor.ORM
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Text { get; set; }
    }
}
