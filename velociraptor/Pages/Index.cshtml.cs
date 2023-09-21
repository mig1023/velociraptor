using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using velociraptor.Model;
using velociraptor.ORM;

namespace velociraptor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<Article> LastArticles { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            LastArticles = Database.All()
                .OrderByDescending(x => x.Created)
                .Take(5)
                .ToList();
        }
    }
}