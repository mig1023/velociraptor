using DiffPlex;
using DiffPlex.Chunkers;
using DiffPlex.Model;
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

        public string Text { get; set; }

        public int Pos { get; set; }

        public ChangeType Type { get; set; }

        public static List<History> Get(string oldText, string newText)
        {
            IDiffer diff = new Differ();
            IChunker chunker = new CharacterChunker();
            DiffResult result = diff.CreateDiffs(oldText, newText, false, false, chunker);

            List<History> histories = new List<History>();

            foreach (DiffBlock item in result.DiffBlocks)
            {
                History history = new History();

                if (item.DeleteCountA > 0)
                {
                    history.Text = oldText.Substring(item.DeleteStartA, item.DeleteCountA);
                    history.Pos = item.DeleteStartA;
                    history.Type = ChangeType.Deleted;
                }

                if (item.InsertCountB > 0)
                {
                    history.Text = newText.Substring(item.InsertStartB, item.InsertCountB);
                    history.Pos = item.InsertStartB;
                    history.Type = ChangeType.Inserted;
                }

                histories.Add(history);
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
