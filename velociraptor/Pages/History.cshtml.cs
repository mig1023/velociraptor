using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using velociraptor.Model;
using velociraptor.ORM;

namespace velociraptor.Pages
{
    public class HistoryModel : PageModel
    {
        public Article Article { get; set; }

        public List<Fragment> Fragments { get; set; }

        public DateTime ChangeDatePrev { get; set; }

        public DateTime ChangeDateNext { get; set; }

        public IActionResult OnGet(string title)
        {
            Article = Database.Get(title);
            List<History> histories = Database.Changes(title);
            ChangeDateNext = histories.Last().Date;
            Fragments = Fragment.Get(Article.Text, histories);

            return Page();
        }
    }
}
