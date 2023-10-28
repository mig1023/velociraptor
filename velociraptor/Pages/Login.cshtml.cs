using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using velociraptor.ORM;

namespace velociraptor.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Required]
        public Model.User User { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Database.VerifyPassword(User.Email, User.Password))
            {
                Session.SetCookies(User.Email, PageContext.HttpContext);
                return RedirectToPage("Index");
            }
            else
            {
                ModelState.AddModelError("User.Email", "Email or Password is wrong");
                return Page();
            }
        }
    }
}
