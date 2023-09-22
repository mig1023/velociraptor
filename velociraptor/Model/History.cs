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

        public static List<Fragment> Diff(string text, List<History> histories)
        {
            List<Fragment> fragments = new List<Fragment>();
            int prevIndex = 0;

            foreach (History history in histories)
            {
                if (history.Pos > prevIndex)
                {
                    string frag = text.Substring(prevIndex, history.Pos - prevIndex);
                    fragments.AddRange(Fragment.Get(frag, 0));
                }

                fragments.AddRange(Fragment.Get(history.Text, history.Type));

                int size_bonus = history.Type == ChangeType.Inserted ? history.Text.Length : 0;
                prevIndex = history.Pos + size_bonus;
            }

            int last = text.Length - prevIndex;
            fragments.AddRange(Fragment.Get(text.Substring(prevIndex, last), 0));

            return fragments;
        }

        public static string Restore(string text, List<History> histories)
        {
            List<Fragment> fragments = Diff(text, histories);
            string prevText = String.Empty;

            foreach (Fragment frag in fragments)
            {
                if (frag.Type != ChangeType.Inserted)
                    prevText += frag.Text;

                if (!frag.NoNewLine)
                    prevText += "\n";
            }

            return prevText;
        }
    }
}
