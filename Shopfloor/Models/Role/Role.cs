namespace Shopfloor.Models.RoleModel
{
    public sealed class Role
    {
        //private readonly RoleDTO _data = new();
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