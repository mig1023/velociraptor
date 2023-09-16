using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace velociraptor.Pages
{
    public class ViewModel : PageModel
    {
        public ORM.Article Article { get; set; }

        public IActionResult OnGet(string title)
        {
            if (ORM.Db.Exists(title, out ORM.Article article))
            {
                Article = article;
                return Page();
            }
            else
            {
                return RedirectToPage("Add", new { title = title });
            }
        }
    }
}
