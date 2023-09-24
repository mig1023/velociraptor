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

        public string NextVersion { get; set; }

        public string PrevVersion { get; set; }

        public IActionResult OnGet(string title, int? version)
        {
            Article = Database.Get(title, version);

            int currentVersion = version ?? Database.LastVersion(title);
            List<History> histories = Database.Changes(title, currentVersion);

            ChangeDateNext = Database.VersionDate(title, currentVersion);

            int prevVersion = Database.PrevVersion(title, currentVersion);
            PrevVersion = Url.Page("History", new { title, version = prevVersion });
            ChangeDatePrev = Database.VersionDate(title, prevVersion);

            Fragments = Fragment.Get(Article.Text, histories);

            return Page();
        }
    }
}
