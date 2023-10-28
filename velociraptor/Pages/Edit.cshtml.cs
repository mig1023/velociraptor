using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using velociraptor.Model;
using velociraptor.ORM;

namespace velociraptor.Pages
{
    [Authorize]
    public class EditModel : PageModel
    {
        [BindProperty]
        public Article Article { get; set; }

        public EditModel()
        {
            Article = new Article();
        }

        public IActionResult OnGet(string title)
        {
            if (Database.Exists(title, out Article article))
            {
                Article = article;
                return Page();
            }
            else
            {
                return RedirectToPage("Add", new { title });
            }
        }

        public IActionResult OnPost(string title)
        {
            Database.Save(Article);
            return RedirectToPage("View", new { title });
        }
    }
}
