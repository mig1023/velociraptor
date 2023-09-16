using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using velociraptor.ORM;

namespace velociraptor.Pages
{
    public class AddModel : PageModel
    {
        [BindProperty]
        [Required]
        public string Title { get; set; }

        public IActionResult OnGet(string title)
        {
            if (!String.IsNullOrEmpty(title) && Db.Exists(title, out Article _))
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

            if (!Db.Exists(Title, out Article _))
                Db.Create(Title);

            return RedirectToPage("Edit", new { title = Title });
        }
    }
}
