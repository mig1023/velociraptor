using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using velociraptor.ORM;

namespace velociraptor.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            Session.SetCookiesOut(PageContext.HttpContext);
            return RedirectToPage("Login");
        }
    }
}
