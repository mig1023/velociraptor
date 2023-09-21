using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using velociraptor.Model;

namespace velociraptor.Pages
{
    public class ViewModel : PageModel
    {
        public Article Article { get; set; }
        public string EditLink { get; set; }

        public IActionResult OnGet(string title)
        {
            if (ORM.Database.Exists(title, out Article article))
            {
                Article = article;
                EditLink = Url.Page("Edit", new { title });
                return Page();
            }
            else
            {
                return RedirectToPage("Add", new { title });
            }
        }
    }
}
