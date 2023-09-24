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

        public Model.Version Next { get; set; }

        public Model.Version Prev { get; set; }


        public IActionResult OnGet(string title, int? version)
        {
            Article = Database.Get(title, version);

            int currentVersion = version ?? Database.LastVersion(title);
            List<History> histories = Database.Changes(title, currentVersion);

            int nextVesrion = Database.OtherVersion(title, currentVersion, next: true);

            Next = new Model.Version
            {
                Number = nextVesrion,
                Link = Url.Page("History", new { title, version = nextVesrion }),
                Date = Database.VersionDate(title, currentVersion),
            };

            int prevVersion = Database.OtherVersion(title, currentVersion, prev: true);

            Prev = new Model.Version
            {
                Number = prevVersion,
                Link = Url.Page("History", new { title, version = prevVersion }),
                Date = Database.VersionDate(title, prevVersion),
            };

            Fragments = Fragment.Get(Article.Text, histories);

            return Page();
        }
    }
}
