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

                if (change != null)
                {
                    List<History> diffChanges = History.Get(change.Id, change.Text, article.Text);
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

        public static bool Exists(string title, out Article article)
        {
            article = Get(title);

            return article != null;
        }
    }
}
