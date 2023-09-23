using velociraptor.Model;

namespace velociraptor.ORM
{
    public class Database
    {
        public static void Create(string article)
        {
            Article newArticle = new Article
            {
                Title = article,
                Text = String.Empty,
                Created = DateTime.Now,
                LastChange = DateTime.Now,
            };

            using (EntityContext db = new EntityContext())
            {
                db.Articles.Add(newArticle);
                db.SaveChanges();
            }
        }

        public static void Save(Article article)
        {
            using (EntityContext db = new EntityContext())
            {
                Article? change = db.Articles.SingleOrDefault(x => x.Title == article.Title);
                int newVersion = Database.LastVersion(change.Title) + 1;

                if (change != null)
                {
                    List<History> diffChanges = History.Get(change.Id, newVersion, change.Text, article.Text);
                    db.Histories.AddRange(diffChanges);

                    change.Text = article.Text;
                    change.LastChange = DateTime.Now;

                    db.SaveChanges();
                }
            }
        }

        public static List<Article> All()
        {
            using (EntityContext db = new EntityContext())
            {
                var articles = db.Articles.ToList();
                return articles;
            }
        }

        public static Article? Get(string title)
        {
            using (EntityContext db = new EntityContext())
            {
                return db.Articles.SingleOrDefault(x => x.Title == title);
            }
        }

        public static List<History> Changes(string title)
        {
            using (EntityContext db = new EntityContext())
            {
                Article? article = db.Articles
                    .SingleOrDefault(x => x.Title == title);

                int lastVersion = Database.LastVersion(title);

                List<History> changes = db.Histories
                    .Where(x => x.ArcticleId == article.Id)
                    .Where(x => x.Version == lastVersion)
                    .ToList();

                return changes;
            }
        }

        public static int LastVersion(string title)
        {
            using (EntityContext db = new EntityContext())
            {
                Article? article = db.Articles
                    .SingleOrDefault(x => x.Title == title);

                int lastVersion = db.Histories
                    .Where(x => x.ArcticleId == article.Id)
                    .Max(x => (int?)x.Version) ?? 0;

                return lastVersion;
            }
        }

        public static int PrevVersion(string title, int version, out DateTime date)
        {
            date = DateTime.Now;

            if (version <= 1)
                return 0;

            using (EntityContext db = new EntityContext())
            {
                Article? article = db.Articles
                    .SingleOrDefault(x => x.Title == title);

                var allVersions = db.Histories
                    .Where(x => x.ArcticleId == article.Id)
                    .Where(x => x.Version < version)
                    .Select(x => x.Version)
                    .Distinct()
                    .OrderByDescending(x => x);
                    

                if (allVersions.Count() < 1)
                    return 0;

                int tmp = allVersions.First();

                date = db.Histories
                    .Where(x => x.Version == allVersions.First())
                    .Select(x => x.Date)
                    .FirstOrDefault();

                return allVersions.First();
            }
        }

        public static bool Exists(string title, out Article article)
        {
            article = Get(title);

            return article != null;
        }
    }
}
