namespace velociraptor.ORM
{
    public class Db
    {
        public static void Create(string article)
        {
            Article newArticle = new Article
            {
                Title = article,
                Text = String.Empty
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
                    change = article;
                    db.SaveChanges();
                }
            }
        }

        public static List<Article> List()
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
    }
}
