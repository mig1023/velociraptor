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
    }
}
