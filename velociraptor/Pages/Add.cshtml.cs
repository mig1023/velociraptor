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

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                ORM.Db.Create(Title);
                return RedirectToPage("Edit", new { title = Title });
            }
        }
    }
}
