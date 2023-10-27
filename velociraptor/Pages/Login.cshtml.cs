using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

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
    }
}
