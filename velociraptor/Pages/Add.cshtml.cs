using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace velociraptor.Pages
{
    public class AddModel : PageModel
    {
        [BindProperty]
        [Required]
        public string Title { get; set; }

        public IActionResult OnGet(string title)
        {
            if (!String.IsNullOrEmpty(title) && ORM.Db.Exists(title, out ORM.Article _))
            {
                return RedirectToPage("Edit", new { title = title });
            }
            else
            {
                Title = title;
                return Page();
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            if (!ORM.Db.Exists(Title, out ORM.Article _))
                ORM.Db.Create(Title);

            return RedirectToPage("Edit", new { title = Title });
        }
    }
}
