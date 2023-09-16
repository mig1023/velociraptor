using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using velociraptor.ORM;

namespace velociraptor.Pages
{
    public class ViewModel : PageModel
    {
        public Article Article { get; set; }
        public string EditLink { get; set; }

        public IActionResult OnGet(string title)
        {
            if (ORM.Db.Exists(title, out Article article))
            {
                Article = article;
                EditLink = Url.Page("Edit", new { title = title });
                return Page();
            }
            else
            {
                return RedirectToPage("Add", new { title = title });
            }
        }
    }
}
