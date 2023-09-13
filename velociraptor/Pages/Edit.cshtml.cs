using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace velociraptor.Pages
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public ORM.Article Article { get; set; }

        public EditModel()
        {
            Article = new ORM.Article();
        }

        public void OnGet(string title)
        {
            Article = ORM.Db.Get(title);
        }

        public void OnPost()
        {
            ORM.Db.Save(Article);
        }
    }
}
