using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace velociraptor.Pages
{
    public class EditModel : PageModel
    {
        public string ArticleTitle { get; set; }

        public void OnGet(string title)
        {
            ArticleTitle = title;
        }
    }
}
