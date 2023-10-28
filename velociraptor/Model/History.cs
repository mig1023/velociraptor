using DiffPlex;
using DiffPlex.Chunkers;
using DiffPlex.Model;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace velociraptor.Model
{
    public class History
    {
        public enum ChangeType
        {
            Nope,
            Deleted,
            Inserted
        }

        [Key]
        public int Id { get; set; }

        public int ArcticleId { get; set; }

        public string Text { get; set; }

        public int Pos { get; set; }

        public int Version { get; set; }

        public string Author { get; set; }

        public DateTime Date { get; set; }

        public ChangeType Type { get; set; }

        public History()
        {
        }

        public History(int articleId, int newVersion, string author)
        {
            this.ArcticleId = articleId;
            this.Version = newVersion;
            this.Author = author;
            this.Date = DateTime.Now;
        }

        public static List<History> Get(int articleId, int newVersion,
            string oldText, string newText, string author)
        {
            IDiffer diff = new Differ();
            IChunker chunker = new CharacterChunker();
            DiffResult result = diff.CreateDiffs(oldText, newText, false, false, chunker);

            List<History> histories = new List<History>();

            foreach (DiffBlock item in result.DiffBlocks)
            {
                if (item.DeleteCountA > 0)
                {
                    History history = new History(articleId, newVersion, author);
                    history.Text = oldText.Substring(item.DeleteStartA, item.DeleteCountA);
                    history.Pos = item.DeleteStartA;
                    history.Type = ChangeType.Deleted;
                    histories.Add(history);
                }

                if (item.InsertCountB > 0)
                {
                    History history = new History(articleId, newVersion, author);
                    history.Text = newText.Substring(item.InsertStartB, item.InsertCountB);
                    history.Pos = item.InsertStartB;
                    history.Type = ChangeType.Inserted;
                    histories.Add(history);
                }
            }

            return histories;
        }

        public static string Restore(string text, List<History> histories)
        {
            List<Fragment> fragments = Fragment.Get(text, histories);
            StringBuilder restored = new StringBuilder();

            foreach (Fragment fragment in fragments)
            {
                if (fragment.Type != ChangeType.Inserted)
                    restored.Append(fragment.Text);

                if (!fragment.NoNewLine)
                    restored.Append("\n");
            }

            return restored.ToString();
        }
    }
}
