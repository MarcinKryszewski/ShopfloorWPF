namespace Shopfloor.Models
{
    public class Role
    {
        public int Id { get; }
        public int Value { get; }
        public string Name { get; }

        public Role(int id, string name, int value)
        {
            Id = id;
            Name = name;
            Value = value;
        }

        public Role(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }
}