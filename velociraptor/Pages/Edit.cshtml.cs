using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using velociraptor.ORM;

namespace velociraptor.Pages
{
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
            if (Db.Exists(title, out Article article))
            {
                Article = article;
                return Page();
            }
            else
            {
                return RedirectToPage("Add", new { title = title });
            }
        }

        public void OnPost()
        {
            Db.Save(Article);
        }
    }
}
