using static velociraptor.Model.History;

namespace velociraptor.Model
{
    public class Fragment
    {
        public string Text { get; set; }

        public ChangeType Type { get; set; }

        public bool NoNewLine { get; set; }

        static List<Fragment> Get(string text, ChangeType type)
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
    }
}
