namespace Shopfloor.Models.RoleModel
{
    public class Role
    {
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
        public int? Id { get; }
        public string Name { get; }
        public int Value { get; }
    }
}