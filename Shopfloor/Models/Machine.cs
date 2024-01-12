namespace Shopfloor.Models
{
    public class Machine
    {
        public int Id { get; }
        public string Name { get; }
        public string Number { get; }
        public int Parent { get; }

        public Machine(int id, string name, string number, int parent)
        {
            Id = id;
            Name = name;
            Number = number;
            Parent = parent;
        }

        public Machine(string name, string number, int parent)
        {
            Name = name;
            Number = number;
            Parent = parent;
        }
    }
}