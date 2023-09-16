using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using velociraptor.ORM;

namespace velociraptor.Pages
{
    public class ViewModel : PageModel
    {
        public Article Article { get; set; }

        public IActionResult OnGet(string title)
        {
            if (ORM.Db.Exists(title, out Article article))
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
