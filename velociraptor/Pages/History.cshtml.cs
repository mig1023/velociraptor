using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using velociraptor.Model;
using velociraptor.ORM;

namespace velociraptor.Pages
{
    public class HistoryModel : PageModel
    {
        public Article Article { get; set; }

        public List<History> Histories { get; set; }

        public IActionResult OnGet(string title)
        {
            Article = Database.Get(title);
            Histories = Database.Changes(title);

            return Page();
        }
    }
}
