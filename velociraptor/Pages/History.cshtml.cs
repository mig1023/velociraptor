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

        public DateTime ChangeDateNext { get; set; }

        public string NextVersionLink { get; set; }

        public int NextVersion { get; set; }

        public DateTime ChangeDatePrev { get; set; }

        public string PrevVersionLink { get; set; }

        public int PrevVersion { get; set; }

        public IActionResult OnGet(string title, int? version)
        {
            Article = Database.Get(title, version);

            int currentVersion = version ?? Database.LastVersion(title);
            List<History> histories = Database.Changes(title, currentVersion);

            int nextVesrion = Database.OtherVersion(title, currentVersion, next: true);
            NextVersion = nextVesrion;
            NextVersionLink = Url.Page("History", new { title, version = nextVesrion });
            ChangeDateNext = Database.VersionDate(title, currentVersion);

            int prevVersion = Database.OtherVersion(title, currentVersion, prev: true);
            PrevVersion = prevVersion;
            PrevVersionLink = Url.Page("History", new { title, version = prevVersion });
            ChangeDatePrev = Database.VersionDate(title, prevVersion);

            Fragments = Fragment.Get(Article.Text, histories);

            return Page();
        }
    }
}
