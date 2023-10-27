using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using velociraptor.ORM;

namespace velociraptor.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public Model.User User {get;set;}

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            Database.Register(User.Email, User.Password);

            return RedirectToPage("Index");
        }
    }
}
