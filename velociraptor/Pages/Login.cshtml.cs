using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
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

        public IActionResult OnPost(HttpContext context)
        {
            if (!ModelState.IsValid)
                return Page();

            if (Database.VerifyPassword(User.Email, User.Password))
            {
                SetCookies(User.Email, context);
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }

        private async void SetCookies(string identity, HttpContext context)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, identity)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }
    }
}
