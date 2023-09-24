﻿using velociraptor.Model;

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

        public static Article? Get(string title, int? version = null)
        {
            using (EntityContext db = new EntityContext())
            {
                Article article = db.Articles
                    .SingleOrDefault(x => x.Title == title);

                if (version == null)
                    return article;


            }
        }

        public static List<History> Changes(string title, int version)
        {
            using (EntityContext db = new EntityContext())
            {
                Article? article = db.Articles
                    .SingleOrDefault(x => x.Title == title);

                List<History> changes = db.Histories
                    .Where(x => x.ArcticleId == article.Id)
                    .Where(x => x.Version == version)
                    .ToList();

                return changes;
            }
        }

        public static List<int> AllVersions(string title)
        {
            using (EntityContext db = new EntityContext())
            {
                Article? article = db.Articles
                    .SingleOrDefault(x => x.Title == title);

                var allVersions = db.Histories
                    .Where(x => x.ArcticleId == article.Id)
                    .Select(x => x.Version)
                    .Distinct()
                    .OrderByDescending(x => x);

                return allVersions.ToList();
            }
        }

        public static int LastVersion(string title)
        {
            int maxVersions = AllVersions(title)
                .Max(x => (int?)x) ?? 0;

            return maxVersions;
        }

        public static int PrevVersion(string title, int version)
        {
            int lastVersions = AllVersions(title)
                .Where(x => x < version)
                .First();

            return lastVersions;
        }

        public static bool Exists(string title, out Article article)
        {
            article = Get(title);

            return article != null;
        }

        public static DateTime VersionDate(string title, int version)
        {
            using (EntityContext db = new EntityContext())
            {
                Article? article = db.Articles
                    .SingleOrDefault(x => x.Title == title);

                DateTime date = db.Histories
                    .Where(x => x.ArcticleId == article.Id)
                    .Where(x => x.Version == version)
                    .Select(x => x.Date)
                    .FirstOrDefault();

                return date;
            }
        }
    }
}
