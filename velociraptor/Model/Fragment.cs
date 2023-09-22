using static velociraptor.Model.History;

namespace velociraptor.Model
{
    public class Fragment
    {
        public string Text { get; set; }

        public ChangeType Type { get; set; }

        public bool NoNewLine { get; set; }

        public static List<Fragment> Get(string text, ChangeType type)
        {
            List<Fragment> fragments = new List<Fragment>();

            if (String.IsNullOrEmpty(text))
                return fragments;

            List<String> lines = text.Split("\n").ToList();

            foreach (string line in lines)
            {
                Fragment fragment = new Fragment
                {
                    Text = line,
                    Type = type,
                };

                fragments.Add(fragment);
            }

            if (!text.EndsWith("\n") && fragments.Count > 0)
                fragments[fragments.Count - 1].NoNewLine = true;

            return fragments;
        }

        public static List<Fragment> Get(string text, List<History> histories)
        {
            List<Fragment> fragments = new List<Fragment>();
            int prevIndex = 0;

            foreach (History history in histories)
            {
                if (history.Pos > prevIndex)
                {
                    string frag = text.Substring(prevIndex, history.Pos - prevIndex);
                    fragments.AddRange(Get(frag, 0));
                }

                fragments.AddRange(Get(history.Text, history.Type));

                int size_bonus = history.Type == ChangeType.Inserted ? history.Text.Length : 0;
                prevIndex = history.Pos + size_bonus;
            }

            int last = text.Length - prevIndex;
            fragments.AddRange(Get(text.Substring(prevIndex, last), 0));

            return fragments;
        }
    }
}
