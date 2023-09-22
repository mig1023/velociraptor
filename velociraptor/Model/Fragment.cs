using static velociraptor.Model.History;

namespace velociraptor.Model
{
    public class Fragment
    {
        public string Text { get; set; }

        public ChangeType Type { get; set; }

        public bool NoNewLine { get; set; }
    }
}
