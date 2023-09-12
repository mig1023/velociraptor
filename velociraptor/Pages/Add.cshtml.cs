using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace velociraptor.Pages
{
    public class AddModel : PageModel
    {
        [BindProperty]
        [Required]
        public string ArticleTitle { get; set; }

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
                return RedirectToPage("Edit", new { title = ArticleTitle });
            }
        }
    }
}
