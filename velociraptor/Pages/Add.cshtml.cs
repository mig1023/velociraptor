using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using velociraptor.Model;
using velociraptor.ORM;

namespace velociraptor.Pages
{
    [Authorize]
    public class AddModel : PageModel
    {
        [BindProperty]
        [Required]
        public string Title { get; set; }

        public IActionResult OnGet(string title)
        {
            if (!String.IsNullOrEmpty(title) && Database.Exists(title, out Article _))
            {
                return RedirectToPage("Edit", new { title });
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

            if (!Database.Exists(Title, out Article _))
                Database.Create(Title);

            return RedirectToPage("Edit", new { Title });
        }
    }
}
