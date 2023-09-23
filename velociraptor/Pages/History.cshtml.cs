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

        public IActionResult OnGet(string title, int? version)
        {
            int historyVersion = version ?? Database.LastVersion(title);

            Article = Database.Get(title);
            List<History> histories = Database.Changes(title, historyVersion);
            Database.PrevVersion(title, historyVersion, out DateTime prevChanges);
            ChangeDatePrev = prevChanges;
            ChangeDateNext = histories.Last().Date;
            Fragments = Fragment.Get(Article.Text, histories);

            return Page();
        }
    }
}
